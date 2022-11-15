using System.Reflection;
using Jcg.Domain.Aggregates.InvarianRuleHandlers;
using Jcg.Domain.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Jcg.Domain.UnitTests.Aggregates.InvariantRuleHandlers
{
    public class InvariantRuleHandlerTypesScannerTests
    {
        private InvariantRuleHandlerTypesScanner Sut { get; } = new();


        [Fact]
        public void ReturnsHandlerTypesForAggregate()
        {
            // ************ ARRANGE ************

            // ************ ACT ****************

            var result = Sut.GetHandlerTypes<AggregateA>(
                Assembly.GetExecutingAssembly());

            // ************ ASSERT *************

            var expected = typeof(InvariantRuleHandlerA1)
                .ToCollection(typeof(InvariantRuleHandlerA2));

            result.ShouldBeEquivalentTo(expected, (x, y) =>
                x == y);
        }
    }
}