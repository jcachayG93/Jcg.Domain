using System.Reflection;
using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Jcg.Domain.UnitTests.Aggregates.DomainEventHandlers
{
    public class DomainEventHandlerTypesScannerTests
    {
        private DomainEventHandlerTypesScanner Sut { get; } = new();

        [Fact]
        public void ReturnsHandlerTypesForAggregate()
        {
            // ************ ARRANGE ************

            // ************ ACT ****************

            var result = Sut.GetHandlerTypes<AggregateB>(
                Assembly.GetExecutingAssembly());

            // ************ ASSERT *************

            var expected = typeof(DomainEventHandlerB1)
                .ToCollection(typeof(DomainEventHandlerB2));

            result.ShouldBeEquivalentTo(expected, (x, y) =>
                x == y);
        }
    }
}
