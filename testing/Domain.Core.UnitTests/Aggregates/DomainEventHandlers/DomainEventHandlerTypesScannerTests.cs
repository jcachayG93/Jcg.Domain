using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates.DomainEventHandlers
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
