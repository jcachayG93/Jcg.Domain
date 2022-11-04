using Domain.Core.Aggregates.InvarianRuleHandlers;

namespace Domain.Core.UnitTests.Types;

public class InvariantRuleHandlerA2 : InvariantRuleHandlerBase<AggregateA>
{
    /// <inheritdoc />
    protected override void AssertEntityStateIsValid(AggregateA aggregate)
    {
        throw new NotImplementedException();
    }
}