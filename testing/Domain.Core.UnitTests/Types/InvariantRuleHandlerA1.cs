using Domain.Core.Aggregates.InvarianRuleHandlers;

namespace Domain.Core.UnitTests.Types
{
    public class InvariantRuleHandlerA1 : InvariantRuleHandlerBase<AggregateA>
    {
        /// <inheritdoc />
        protected override void AssertEntityStateIsValid(AggregateA aggregate)
        {
            throw new NotImplementedException();
        }
    }
}