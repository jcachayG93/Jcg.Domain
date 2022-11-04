using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.UnitTests.Types;

public class DomainEventHandlerA1 : DomainEventHandlerBase<AggregateA>
{
    protected override bool PerformHandling(AggregateA aggregate, IDomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}