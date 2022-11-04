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
