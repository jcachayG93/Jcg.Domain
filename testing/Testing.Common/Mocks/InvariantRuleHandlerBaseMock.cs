using Jcg.Domain.Aggregates;
using Jcg.Domain.Aggregates.InvarianRuleHandlers;

namespace Testing.Common.Mocks
{
    public class InvariantRuleHandlerBaseMock<TAggregate>
        : InvariantRuleHandlerBase<TAggregate>
        where TAggregate : AggregateRootBase
    {
        public TAggregate? HandleArgs { get; private set; } = null;

        /// <inheritdoc />
        protected override void AssertEntityStateIsValid(TAggregate aggregate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override void Handle(TAggregate aggregate)
        {
            HandleArgs = aggregate;
        }
    }
}