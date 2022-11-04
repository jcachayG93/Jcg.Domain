using System.Reflection;
using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.AggregateTests.Types
{
    public class AggregateDouble
        : AggregateRootBase
    {
        public AggregateDouble(Guid id, Assembly assemblyContainingHandlers)
        {
            Id = id;
            _assemblyContainingHandlers = assemblyContainingHandlers;
        }

        public IDomainEvent? Test_WhenArgs { get; private set; } = null;
        public Guid Id { get; }


        public string Name { get; set; } = "";

        protected override void When(IDomainEvent domainEvent)
        {
            Test_WhenArgs = domainEvent;
        }

        /// <inheritdoc />
        protected override void AssertEntityStateIsValud()
        {
            throw new NotImplementedException();
        }

        protected override Guid GetId()
        {
            return Id;
        }

        private readonly Assembly _assemblyContainingHandlers;
    }
}