using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.Aggregates.DomainEventHandlers
{
    /// <summary>
    /// A Chain of responsibility handler that apply a domain event to update the aggregate state
    /// </summary>
    public abstract class DomainEventHandlerBase
    <TAggregate> where TAggregate: AggregateRootBase
    {
        /// <summary>
        /// The Chain of responsibility handle method, will run the request thru each handler until one handles it.
        /// </summary>
        /// <param name="aggregate">The aggregate whose state will be mutated</param>
        /// <param name="domainEvent">The event that describes how to mutate the aggregate state</param>
        internal virtual void Handle(TAggregate aggregate, IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gives this handler an opportunity to handle the event.
        /// </summary>
        /// <param name="aggregate">The aggregate whose state will be mutated</param>
        /// <param name="domainEvent">The event that describes how to mutate the aggregate state</param>
        /// <returns>True if the event was handled, false to delegate to the next handler in the pipeline</returns>
        protected abstract bool PerformHandling(TAggregate aggregate, IDomainEvent domainEvent);

        /// <summary>
        /// Sets the next handler
        /// </summary>
        internal void SetNext(DomainEventHandlerBase<TAggregate> nextHandler)
        {
            NextHandler = nextHandler;
        }

        internal DomainEventHandlerBase<TAggregate>? NextHandler { get; private set; } = null;
    }
}
