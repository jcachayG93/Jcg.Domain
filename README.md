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
The aggregate must inherit the **AggregateRootBase** class. For the DomainEventHandlers
```
internal class Customer : AggregateRootBase
    {
        public Customer(Guid id, string name)
        {
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
            var pipeline = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Customer>()!;

            pipeline.Handle(this, domainEvent);
        }

        /// <inheritdoc />
        protected override void AssertEntityStateIsValid()
        {
            var pipeline = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly())
                .GetPipeline<Customer>();

            pipeline.Handle(this);
        }

        /// <inheritdoc />
        protected override Guid GetId()
        {
            return Id;
        }
    }
```
