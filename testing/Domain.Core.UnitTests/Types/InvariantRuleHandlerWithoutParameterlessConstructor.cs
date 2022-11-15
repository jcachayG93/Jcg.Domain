using Jcg.Domain.Aggregates.InvarianRuleHandlers;

namespace Jcg.Domain.UnitTests.Types;

public class InvariantRuleHandlerWithoutParameterlessConstructor
    : InvariantRuleHandlerBase<AggregateB>
{
    public InvariantRuleHandlerWithoutParameterlessConstructor(string value)
    {
    }

    /// <inheritdoc />
    protected override void AssertEntityStateIsValid(AggregateB aggregate)
    {
        throw new NotImplementedException();
    }
}