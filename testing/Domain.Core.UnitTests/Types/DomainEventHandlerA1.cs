using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Aggregates.Handlers;

namespace Domain.Core.UnitTests.Types;

public class DomainEventHandlerA1 : DomainEventHandlerBase<AggregateA>
{
    protected override bool PerformHandling(AggregateA aggregate, IDomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}