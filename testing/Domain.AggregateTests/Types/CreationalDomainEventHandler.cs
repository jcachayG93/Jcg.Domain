using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.AggregateTests.Types;

public class CreationalDomainEventHandler : DomainEventHandlerBase<AggregateDouble>
{
    protected override bool PerformHandling(AggregateDouble aggregate, IDomainEvent domainEvent)
    {
        if (domainEvent is CreationalDomainEvent cev)
        {
            aggregate.Name = cev.Name;
            return true;
        }

        return false;
    }
}