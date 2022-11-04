using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.Aggregates
{
    /// <summary>
    /// The base class for an aggregate root
    /// </summary>
    public abstract class AggregateRootBase
    {
        /// <summary>
        /// The Aggregate version is incremented each time a command is applied
        /// </summary>
        public long Version { get; private set; }

        /// <summary>
        /// Contains all the domain events that have been applied to the aggregate
        /// </summary>
        public IDomainEvent[] Changes => _changes.ToArray();

        private List<IDomainEvent> _changes = new();

        /// <summary>
        /// Resets the Changes collection
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ResetChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Applies a domain event to update the state, this has the following effects:
        /// 1. If the event is NonCreational type, asserts that the event's AggregateId matches this aggregate Id.
        /// 2. Delegates to the Domain Event handlers pipeline to update the Aggregate state
        /// 3. Delegates to the Invariants pipeline to assert the state is valid
        /// 4. Increments the Version value
        /// 5. Adds the event to the Changes collection
        /// </summary>
        public void Apply(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The base class uses this method to get the Id value for this aggregate
        /// </summary>
        protected abstract Guid GetId();

        /// <summary>
        /// The base class uses this method to get the assembly that contains the handlers so
        /// it can assembly a handlers pipelines (for invariants and domain event handlers)
        /// </summary>
        /// <returns></returns>
        protected abstract Assembly GetAssemblyContainingHandlers();
    }
}
