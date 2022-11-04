using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Exceptions;

namespace Domain.Core.Aggregates
{
    /// <summary>
    ///     The base class for an aggregate root
    /// </summary>
    public abstract class AggregateRootBase
    {
        /// <summary>
        ///     The Aggregate version is incremented each time a command is applied
        /// </summary>
        public long Version { get; private set; }

        /// <summary>
        ///     Contains all the domain events that have been applied to the aggregate
        /// </summary>
        public IDomainEvent[] Changes => _changes.ToArray();

        /// <summary>
        ///     Resets the Changes collection
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ResetChanges()
        {
            _changes.RemoveAll(e => true);
        }

        /// <summary>
        ///     Applies a domain event to update the state, this has the following effects:
        ///     1. If the event is NonCreational type, asserts that the event's AggregateId matches this aggregate Id.
        ///     2. Delegates to the Domain Event handlers pipeline to update the Aggregate state
        ///     3. Delegates to the Invariants pipeline to assert the state is valid
        ///     4. Increments the Version value
        ///     5. Adds the event to the Changes collection
        /// </summary>
        public void Apply(IDomainEvent domainEvent)
        {
            if (domainEvent is INonCreationalDomainEvent &&
                domainEvent.AggregateId != GetId())
            {
                throw new AggregateIdDoesNotMatchForNonCreationalEventException(
                    GetId(), domainEvent.AggregateId,
                    domainEvent.GetType().FullName!);
            }

            When(domainEvent);

            Version++;

            _changes.Add(domainEvent);
        }

        /// <summary>
        ///     Handle the event using the domain event handler pipeline
        /// </summary>
        /// <param name="domainEvent"></param>
        protected abstract void When(IDomainEvent domainEvent);

        /// <summary>
        ///     Runs this aggregate thru the InvariantRuleHandlers pipeline to assert it's state is valid
        /// </summary>
        protected abstract void AssertEntityStateIsValud();

        /// <summary>
        ///     The base class uses this method to get the Id value for this aggregate
        /// </summary>
        protected abstract Guid GetId();

        private readonly List<IDomainEvent> _changes = new();
    }
}