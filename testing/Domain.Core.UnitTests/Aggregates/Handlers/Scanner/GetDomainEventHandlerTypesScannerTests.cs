using System.Reflection;
using Domain.Core.Aggregates.Handlers.Scanner;
using Domain.Core.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates.Handlers.Scanner
{
    public class GetDomainEventHandlerTypesScannerTests
    {
        private GetDomainEventHandlerTypesScanner CreateSut()
        {
            return new();
        }

        [Fact]
        public void GetsAllHandlersForAggregateA()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            // ************ ACT ****************

            var handlers = sut.GetDomainEventHandlerTypes(typeof(AggregateA),
                Assembly.GetExecutingAssembly());

            // ************ ASSERT *************

            var expected =
                typeof(DomainEventHandlerA1).ToCollection(
                    typeof(DomainEventHandlerA2));

            handlers.ShouldBeEquivalentTo(expected, (x, y) => x == y);
        }

        [Fact]
        public void GetsAllHandlersForAggregateB()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            // ************ ACT ****************

            var handlers = sut.GetDomainEventHandlerTypes(typeof(AggregateB),
                Assembly.GetExecutingAssembly());

            // ************ ASSERT *************

            var expected =
                typeof(DomainEventHandlerB1).ToCollection();

            handlers.ShouldBeEquivalentTo(expected, (x, y) => x == y);
        }
    }
}