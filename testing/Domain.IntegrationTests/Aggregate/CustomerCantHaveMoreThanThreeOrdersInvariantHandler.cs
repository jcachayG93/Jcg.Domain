using Jcg.Domain.Aggregates.InvarianRuleHandlers;

namespace Domain.IntegrationTests.Aggregate
{
    internal class CustomerCantHaveMoreThanThreeOrdersInvariantHandler
        : InvariantRuleHandlerBase<Customer>
    {
        /// <inheritdoc />
        protected override void AssertEntityStateIsValid(Customer aggregate)
        {
            if (aggregate.Orders.Count > 3)
            {
                throw new CustomerHasMoreThanThreeOrdersException();
            }
        }
    }
}