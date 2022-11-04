using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.Aggregates.Handlers
{
    /// <summary>
    /// A handler that applies a domain event to the Aggregate
    /// </summary>
    public abstract class DomainEventHandlerBase<TAggregate>
    where TAggregate : AggregateRootBase
    {
        /// <summary>
        /// Handle the domain event to update the aggregte state. This aggregate is part of a pipeline.
        /// </summary>
        /// <param name="aggregate">The aggregate that will be updated</param>
        /// <param name="domainEvent">The domain event, which can be Creational or NonCreational type</param>
        /// <param name="wasHandled">True if the domain event was handled, false to delegate to the next handler in the pipeline</param>
        protected abstract void PerformHandling(TAggregate aggregate, IDomainEvent domainEvent, out bool wasHandled);

        internal void Handle(TAggregate aggregate, IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }

        internal void SetNext(DomainEventHandlerBase<TAggregate> nextHandler)
        {
            throw new NotImplementedException();
        }
    }
}
