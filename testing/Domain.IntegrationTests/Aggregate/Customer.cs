using System.Reflection;
using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Aggregates.InvarianRuleHandlers;

namespace Domain.IntegrationTests.Aggregate
{
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
}