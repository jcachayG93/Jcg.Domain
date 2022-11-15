namespace Jcg.Domain.Aggregates.InvarianRuleHandlers
{
    /// <summary>
    ///     This default handler is returned by the InvariantRuleHandler...Provider when no handlers were found in the
    ///     assembly.
    ///     This handler does not throw an exception when called.
    ///     Some use cases may have an aggregate that does not have Invariant rules or they have not been implemented yet, this
    ///     handler covers that user case.
    /// </summary>
    /// <typeparam name="TAggregate"></typeparam>
    internal class
        DefaultInvariantRuleHandler<TAggregate> : InvariantRuleHandlerBase<
            TAggregate>
        where TAggregate : AggregateRootBase
    {
        /// <inheritdoc />
        protected override void AssertEntityStateIsValid(TAggregate aggregate)
        {
            // nothing here
        }
    }
}