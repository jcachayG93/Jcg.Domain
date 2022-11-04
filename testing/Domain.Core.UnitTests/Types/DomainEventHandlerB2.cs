using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.UnitTests.Types;

public class DomainEventHandlerB2 : DomainEventHandlerBase<AggregateB>
{
    protected override bool PerformHandling(AggregateB aggregate, IDomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}