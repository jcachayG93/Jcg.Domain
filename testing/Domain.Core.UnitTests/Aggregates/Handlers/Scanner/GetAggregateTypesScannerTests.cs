using System.Reflection;
using Domain.Core.Aggregates.Handlers.Scanner;
using Domain.Core.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates.Handlers.Scanner
{
    public class GetAggregateTypesScannerTests
    {
        [Fact]
        public void ReturnsAllAggregatesInAssembly()
        {
            // ************ ARRANGE ************

            var sut = new GetAggregateTypesScanner();

            // ************ ACT ****************

            var result = sut.GetAggregateTypes(Assembly.GetExecutingAssembly());

            // ************ ASSERT *************

            var expected = typeof(AggregateA).ToCollection(typeof(AggregateB));

            result.ShouldBeEquivalentTo(expected, (x, y) =>
                x == y);
        }
    }
}