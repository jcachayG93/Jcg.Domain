using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Exceptions;

namespace Domain.Core.Aggregates.Handlers
{
    /// <summary>
    ///     A handler that applies a domain event to the Aggregate
    /// </summary>
    public abstract class DomainEventHandlerBase<TAggregate>
        where TAggregate : AggregateRootBase
    {
        /// <summary>
        ///     Handle the domain event to update the aggregte state. This aggregate is part of a pipeline.
        /// </summary>
        /// <param name="aggregate">The aggregate that will be updated</param>
        /// <param name="domainEvent">The domain event, which can be Creational or NonCreational type</param>
        /// <param name="wasHandled">True if the domain event was handled, false to delegate to the next handler in the pipeline</param>
        protected abstract void PerformHandling(
            TAggregate aggregate, IDomainEvent domainEvent,
            out bool wasHandled);

        internal virtual void Handle(TAggregate aggregate, IDomainEvent domainEvent)
        {
            PerformHandling(aggregate, domainEvent, out var wasHandled);

            if (wasHandled)
            {
                return;
            }

            if (_next is null)
            {
                throw new UnhandledDomainEventException(domainEvent);
            }

            _next.Handle(aggregate, domainEvent);
        }

        internal void SetNext(DomainEventHandlerBase<TAggregate> nextHandler)
        {
            _next = nextHandler;
        }

        private DomainEventHandlerBase<TAggregate>? _next = null;
    }
}