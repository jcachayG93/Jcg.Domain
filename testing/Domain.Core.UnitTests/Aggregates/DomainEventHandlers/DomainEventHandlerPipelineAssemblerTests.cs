using FluentAssertions;
using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Exceptions;
using Jcg.Domain.UnitTests.TestCommon;
using Jcg.Domain.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Jcg.Domain.UnitTests.Aggregates.DomainEventHandlers
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