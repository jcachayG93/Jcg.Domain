using FluentAssertions;
using Jcg.Domain.Aggregates.InvarianRuleHandlers;
using Jcg.Domain.Exceptions;
using Jcg.Domain.UnitTests.TestCommon;
using Jcg.Domain.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Jcg.Domain.UnitTests.Aggregates.InvariantRuleHandlers
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