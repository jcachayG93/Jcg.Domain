using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Aggregates.Handlers;

namespace Domain.Core.UnitTests.Types;

public class DomainEventHandlerB1 : IDomainEventHandler<AggregateB>
{
    /// <inheritdoc />
    public bool PerformHandling(AggregateB aggregate,
        IDomainEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}