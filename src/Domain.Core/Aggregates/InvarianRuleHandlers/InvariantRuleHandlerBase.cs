namespace Domain.Core.Aggregates.InvarianRuleHandlers
{
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

        protected abstract void AssertEntityStateIsValid(TAggregate aggregate);

        internal void SetNext(InvariantRuleHandlerBase<TAggregate> nextHandler)
        {
            NextHandler = nextHandler;
        }
    }
}