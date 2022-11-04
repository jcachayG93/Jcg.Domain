using System.Reflection;
using Domain.Core.Aggregates.InvarianRuleHandlers;
using Domain.Core.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates.InvariantRuleHandlers
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