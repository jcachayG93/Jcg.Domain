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

            var pipeline = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Customer>()!;

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