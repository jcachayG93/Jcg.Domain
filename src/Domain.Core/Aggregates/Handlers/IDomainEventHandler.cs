using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.Aggregates.Handlers
{
    /// <summary>
    ///     Handles a domain event, this library will build a
    ///     pipeline (chain of responsibility) with all the handlers for the
    ///     aggregate type. For this to work, the implementation of this interface
    ///     must have a parameterless constructor.
    /// </summary>
    public interface IDomainEventHandler<in TAggregate>
        where TAggregate : AggregateRootBase
    {
        /// <summary>
        ///     If this handler is intended to be used with the specified
        ///     domain event, updates the aggregate state
        /// </summary>
        /// <param name="aggregate">The aggregate</param>
        /// <param name="domainEvent">The domain event which can be Creational or NonCreational</param>
        /// <returns>
        ///     Return true if the event was handled, return false to delegate handling to
        ///     the next handler in the pipeline
        /// </returns>
        bool PerformHandling(
            TAggregate aggregate, IDomainEvent domainEvent);
    }
}