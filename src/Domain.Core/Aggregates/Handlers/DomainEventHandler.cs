using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Exceptions;

namespace Domain.Core.Aggregates.Handlers;

/// <summary>
///     Chain of responsibility handler, used to build a pipeline that
///     handles the domain events for an aggregate
/// </summary>
/// <typeparam name="TAggregate">The type of the aggregate</typeparam>
internal class
    DomainEventHandler<TAggregate> : IDomainEventHandlerPipeline<TAggregate>
    where TAggregate : AggregateRootBase
{
    /*
     * This is a Chain of responsibility handler, that delegates the handling itself to the
     * handler strategy supplied via the constructor
     */
    public DomainEventHandler(IDomainEventHandler<TAggregate> handlerStrategy)
    {
        HandlerStrategy = handlerStrategy;
    }

    public IDomainEventHandler<TAggregate> HandlerStrategy { get; }

    public void Handle(TAggregate aggregate, IDomainEvent domainEvent)
    {
        var handled = HandlerStrategy.PerformHandling(aggregate, domainEvent);

        if (handled)
        {
            return;
        }

        if (_nextHandler is null)
        {
            throw new UnhandledDomainEventException(domainEvent);
        }

        _nextHandler.PerformHandling(aggregate, domainEvent);
    }

    public void SetNext(IDomainEventHandler<TAggregate> nextHandler)
    {
        _nextHandler = nextHandler;
    }

    private IDomainEventHandler<TAggregate>? _nextHandler = null;
}