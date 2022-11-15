using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Aggregates.DomainEvents;

namespace Jcg.Domain.UnitTests.Types
{
    public class DomainEventHandlerWithoutParameterlessConstructor
        : DomainEventHandlerBase<AggregateA>
    {
        public DomainEventHandlerWithoutParameterlessConstructor(
            string someValue)
        {
        }

        protected override bool PerformHandling(AggregateA aggregate,
            IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}