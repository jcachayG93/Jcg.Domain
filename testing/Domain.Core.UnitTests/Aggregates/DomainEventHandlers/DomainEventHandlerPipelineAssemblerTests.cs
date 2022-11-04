using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Exceptions;
using Domain.Core.UnitTests.TestCommon;
using Domain.Core.UnitTests.Types;
using FluentAssertions;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates.DomainEventHandlers
{
    public class DomainEventHandlerPipelineAssemblerTests
    {
        private DomainEventHandlerPipelineAssembler CreateSut()
        {
            return new();
        }

        [Fact]
        public void
            AnyHandlerDoesNotHaveAParameterlessConstructor_ThrowsException()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var handlerTypes =
                typeof(DomainEventHandlerWithoutParameterlessConstructor)
                    .ToCollection();

            // ************ ACT ****************

            var act = new Action(() =>
            {
                sut.AssemblePipeline<AggregateA>(handlerTypes);
            });

            // ************ ASSERT *************

            act.Should()
                .Throw<
                    DomainEventHandlerParameterlessConstructorNotFoundException>();
        }


        [Fact]
        public void CreatesAPipelineThatContainsAllHandlersForAggregate()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var handlerTypes =
                typeof(DomainEventHandlerA1).ToCollection(
                    typeof(DomainEventHandlerA2));

            // ************ ACT ****************

            var result = sut.AssemblePipeline<AggregateA>(handlerTypes);

            // ************ ASSERT *************

            var handlers = ExtractDomainEventHandlersHelper
                .ExtractHandlers<AggregateA>(result);

            handlers.ShouldBeEquivalentTo(handlerTypes, (x, y) =>
                x.GetType() == y);
        }
    }
}