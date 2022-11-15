using System.Reflection;
using Jcg.Domain.Aggregates;
using Jcg.Domain.Aggregates.DomainEvents;

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

        public bool Test_AssertEntityStateWasIsValidCalled { get; set; }
        public Guid Id { get; }


        public string Name { get; set; } = "";

        protected override void When(IDomainEvent domainEvent)
        {
            Test_WhenArgs = domainEvent;
        }

        /// <inheritdoc />
        protected override void AssertEntityStateIsValid()
        {
            Test_AssertEntityStateWasIsValidCalled = true;
        }

        protected override Guid GetId()
        {
            return Id;
        }

        private readonly Assembly _assemblyContainingHandlers;
    }
}