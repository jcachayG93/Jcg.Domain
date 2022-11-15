using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Aggregates.DomainEvents;

namespace Jcg.Domain.UnitTests.Types;

public class DomainEventHandlerA2 : DomainEventHandlerBase<AggregateA>
{
    protected override bool PerformHandling(AggregateA aggregate, IDomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}