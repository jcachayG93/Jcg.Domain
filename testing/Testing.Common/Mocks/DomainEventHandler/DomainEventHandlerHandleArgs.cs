using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEvents;

namespace Testing.Common.Mocks.DomainEventHandler;

public record DomainEventHandlerHandleArgs<TAggregate>
    (TAggregate Aggregate, IDomainEvent DomainEvent)
    where TAggregate : AggregateRootBase;