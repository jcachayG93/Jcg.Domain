using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Aggregates.DomainEvents;

namespace Jcg.Domain.UnitTests.Types;

public class DomainEventHandlerB2 : DomainEventHandlerBase<AggregateB>
{
    protected override bool PerformHandling(AggregateB aggregate, IDomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}