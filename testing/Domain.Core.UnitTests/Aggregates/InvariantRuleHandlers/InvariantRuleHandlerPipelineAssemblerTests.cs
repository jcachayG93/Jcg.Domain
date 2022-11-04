using Domain.Core.Aggregates.InvarianRuleHandlers;
using Domain.Core.Exceptions;
using Domain.Core.UnitTests.TestCommon;
using Domain.Core.UnitTests.Types;
using FluentAssertions;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates.InvariantRuleHandlers
{
    public class InvariantRuleHandlerPipelineAssemblerTests
    {
        private InvariantRuleHandlerPipelineAssembler CreateSut()
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
                typeof(InvariantRuleHandlerWithoutParameterlessConstructor)
                    .ToCollection();

            // ************ ACT ****************

            var act = new Action(() =>
            {
                sut.AssemblePipeline<AggregateB>(handlerTypes);
            });

            // ************ ASSERT *************

            act.Should()
                .Throw<
                    DomainEventHandlerParameterlessConstructorNotFoundException>();
        }

        [Fact]
        public void NoHandlers_ReturnsNull()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            // ************ ACT ****************

            var result =
                sut.AssemblePipeline<AggregateA>(new List<Type>());

            // ************ ASSERT *************

            result.Should().BeNull();
        }

        [Fact]
        public void CreatesAPipelineThatContainsAllHandlersForAggregate()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var handlerTypes =
                typeof(InvariantRuleHandlerA1).ToCollection(
                    typeof(InvariantRuleHandlerA2));

            // ************ ACT ****************

            var result = sut.AssemblePipeline<AggregateA>(handlerTypes);

            // ************ ASSERT *************

            var handlers =
                ExtractInvariantRuleHandlersHelper
                    .ExtractHandlers<AggregateA>(result);

            handlers.ShouldBeEquivalentTo(handlerTypes,
                (x, y) =>
                    x.GetType() == y);
        }
    }
}