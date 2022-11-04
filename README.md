# JCG-DOMAIN-CORE

Abstracts complexity from a domain layer by decoupling the DomainEventHandlers and
InvariantRuleHandlers into sepparate classes that are assembled into pipelines.

## Features

### EventHandlersPipeline
A pipeline (Chain of responsibility pattern) consisting on handlers that update the Aggretate state by processing domain events.
This pipeline is assembled automatically via reflection.

### InvariantRulesPipeline
A pipeline (Chain of responsibility pattern) consisting on handlers that assert the aggregate valid state.
This pipeline is assembled automatically via reflection.

## Usage

### Create domain events
There are two type of events: Creational (Initializes an aggregate) and Non-Creational (Updates an aggregate).
When applying a Non-Creational domain event, it's AggregateId value must match the Aggregate Id otherwise an exception will be thrown. This is not the case when the event is Creational

Creational domain event:
```
public record CustomerCreated
        (Guid AggregateId, string Name) : ICreationalDomainEvent;
```

NonCreational domain event:
```
public record CustomerNameUpdated
        (Guid AggregateId, string Name) : INonCreationalDomainEvent;
```
### Create an agregate
- The aggregate must inherit the **AggregateRootBase** class. For the DomainEventHandlers.
- The Aggregate needs to give access so the domain event handlers can mutate it's state. (one way to do it is make the Aggregate class internal and expose it as an interface to other layers)

```
internal class Customer : AggregateRootBase
    {
        public Customer(Guid id, string name)
        {
            // This is how to apply a domain event to mutate state.
            var ev = new DomainEvents.CustomerCreated(id, name);

            Apply(ev);
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public List<Order> Orders { get; } = new();

        public void AddOrder(Guid orderId)
        {
            var ev = new DomainEvents.OrderAdded(Id, orderId);

            Apply(ev);
        }

        public void Update(string name)
        {
            var ev = new DomainEvents.CustomerNameUpdated(Id, name);

            Apply(ev);
        }

        /// <inheritdoc />
        protected override void When(IDomainEvent domainEvent)
        {
            // This is called when a domain event needs to be applied.

            // The DomainEventHandlingPipelineProvider is a singleton.
            // The Pipeline will be created only the first time is called, and cached for as long as the application lives.
            // There is a pipeline for each type of aggregate (classes than derive from AggregateRootBase class)

            // The assembly must be the assembly that contain the handlers

            var pipeline = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Customer>();

            // This method updates this aggregate state.
            pipeline.Handle(this, domainEvent);
        }

        /// <inheritdoc />
        protected override void AssertEntityStateIsValid()
        {
            // Very similar to the DomainEventHandlers pipeline but this one asserts the entity state. Each handler represents a different rule.

            var pipeline = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Customer>();

            pipeline.Handle(this);
        }

        // This method is used by the base class to get the Aggregate identity so it can check the Id matches the Non-Creational domain events.
        /// <inheritdoc />
        protected override Guid GetId()
        {
            return Id;
        }
    }
```

**Version, and Changes list**

The AggregateRootBase class has the following members:

```
public abstract class AggregateRootBase
{
    // Increments when a domain event is applied. For a newly created aggregate it's value is zero.
    public long Version { get; protected set; }

    // This are the DomainEvents (Creational and NonCreational) that were applied
    public IDomainEvent[] Changes {get;}

    // Empties the Changes collection
    public void ResetChanges()
}
```

### Domain Event Handlers

Create DomainEventHandlers for each domain event type.

These handlers must fulfill the following conditions:
1. Must be a class derived from DomainEventHandlerBase<TAggregate>
2. Non-Abstract, Non-Generic class
3. Stateless (the same handlers will be used for any number of aggregate instances)
4. Must have a parameterless constructor
5. Must be located in the assembly that is provided to the DomainEventHandlingPipelineProvider class.

```
internal class CustomerCreatedHandler : DomainEventHandlerBase<Customer>
{
   
    protected override bool PerformHandling(Customer aggregate,
        IDomainEvent domainEvent)
    {
        if (domainEvent is DomainEvents.CustomerCreated cev)
        {
            aggregate.Id = cev.AggregateId;
            aggregate.Name = cev.Name;

            return true;
        }

        // The return value indicates whether or not the event was handled
        return false;
    }
}
```

The **DomainEventHandlingPipelineProvider** class is a singleton that will assemble the pipeline with the handlers it finds in the assembly,
this is done in the AggregateRootBase's When method as in the example above

```

        protected override void When(IDomainEvent domainEvent)
        {
            
            // The DomainEventHandlingPipelineProvider is a singleton.
            // The Pipeline will be created only the first time is called, and cached for as long as the application lives.
            // There is a pipeline for each type of aggregate (classes than derive from AggregateRootBase class)

            // The assembly must be the assembly that contain the handlers

            var pipeline = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Customer>();

            // This method updates this aggregate state.
            pipeline.Handle(this, domainEvent);
        }
```

### Invariant Rule Handlers

Create Invariant Rule Handlers for each aggregate and invariant rule types

These handlers must fulfill the following conditions:
1. Must a class derived from InvariantRuleHandlerBase<TAggregate>
1. Non-Abstract, Non-Generic class
2. Stateless (the same handlers will be used for any number of aggregate instances)
3. Must have a parameterless constructor
4. Must be located in the assembly that is provided to the DomainEventHandlingPipelineProvider class.

**Extract from the Aggregate implementation (derived from AggregateRootClass)**
```
internal class CustomerCantHaveMoreThanThreeOrdersInvariantHandler
        : InvariantRuleHandlerBase<Customer>
    {
        /// <inheritdoc />
        protected override void AssertEntityStateIsValid(Customer aggregate)
        {
            if (aggregate.Orders.Count > 3)
            {
                throw new CustomerHasMoreThanThreeOrdersException();
            }
        }
    }
```

The **InvariantRuleHandlingPipelineProvider** class is a singleton that will assemble the pipeline with the handlers it finds in the assembly,
this is done in the AggregateRootBase's AssertEntityState method as in the example above

**Extract from the Aggregate implementation (derived from AggregateRootClass)**
```
protected override void AssertEntityStateIsValid()
        {
            // Very similar to the DomainEventHandlers pipeline but this one asserts the entity state. Each handler represents a different rule.

            var pipeline = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Customer>();

            pipeline.Handle(this);
        }
```

If there are no invariant rule handlers present in the assembly, the pipeline will consist on a single default handler that does not throw any exception.