using Jcg.Domain.Aggregates.InvarianRuleHandlers;

namespace Jcg.Domain.UnitTests.Types;

public class InvariantRuleHandlerB1 : InvariantRuleHandlerBase<AggregateB>
{
    /// <inheritdoc />
    protected override void AssertEntityStateIsValid(AggregateB aggregate)
    {
        throw new NotImplementedException();
    }
}