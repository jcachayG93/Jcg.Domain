using Domain.Core.Aggregates.InvarianRuleHandlers;

namespace Domain.Core.UnitTests.Types;

public class InvariantRuleHandlerB1 : InvariantRuleHandlerBase<AggregateB>
{
    /// <inheritdoc />
    protected override void AssertEntityStateIsValid(AggregateB aggregate)
    {
        throw new NotImplementedException();
    }
}