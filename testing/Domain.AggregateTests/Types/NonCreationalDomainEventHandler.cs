using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.AggregateTests.Types;

public class NonCreationalDomainEventHandler : DomainEventHandlerBase<AggregateDouble>
{
    protected override bool PerformHandling(AggregateDouble aggregate, IDomainEvent domainEvent)
    {
        if (domainEvent is NonCreationalDomainEvent cev)
        {
            aggregate.Name = cev.Name;
            return true;
        }

        return false;
    }
}