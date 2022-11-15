using Jcg.Domain.Aggregates;
using Jcg.Domain.Aggregates.DomainEvents;

namespace Testing.Common.Mocks.DomainEventHandler;

public record DomainEventHandlerHandleArgs<TAggregate>
    (TAggregate Aggregate, IDomainEvent DomainEvent)
    where TAggregate : AggregateRootBase;