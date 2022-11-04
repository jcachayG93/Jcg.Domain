using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.Aggregates.Handlers;

internal interface IDomainEventHandlerPipeline<TAggregate>
    where TAggregate : AggregateRootBase
{
    void Handle(TAggregate aggregate, IDomainEvent domainEvent);
}