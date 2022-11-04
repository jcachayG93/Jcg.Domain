namespace Domain.Core.Aggregates.InvarianRuleHandlers
{
    /// <summary>
    ///     A Chain of responsibility handler that assert an aggregate state
    /// </summary>
    /// <typeparam name="TAggregate">The Aggregate type</typeparam>
    public abstract class InvariantRuleHandlerBase<TAggregate>
        where TAggregate : AggregateRootBase
    {
        internal InvariantRuleHandlerBase<TAggregate>? NextHandler
        {
            get;
            private set;
        } = null;

        public virtual void Handle(TAggregate aggregate)
        {
            AssertEntityStateIsValid(aggregate);

            if (NextHandler != null)
            {
                NextHandler.Handle(aggregate);
            }
        }

        /// <summary>
        ///     Throw an exceptin if the entity state is invalid
        /// </summary>
        /// <param name="aggregate"></param>
        protected abstract void AssertEntityStateIsValid(TAggregate aggregate);

        internal void SetNext(InvariantRuleHandlerBase<TAggregate> nextHandler)
        {
            NextHandler = nextHandler;
        }
    }
}