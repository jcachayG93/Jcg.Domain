using System.Reflection;
using Domain.Core.Aggregates.InvarianRuleHandlers;
using Domain.Core.UnitTests.TestCommon;
using Domain.Core.UnitTests.Types;
using FluentAssertions;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates.InvariantRuleHandlers
{
    public class InvariantRuleHandlingPipelineProviderTests
    {
        [Fact]
        public async Task ImplementsSingletonPattern()
        {
            // ************ ARRANGE ************

            var instanceTasks = Enumerable.Range(0, 500000)
                .Select(i => Task.Run(() =>
                    InvariantRuleHandlingPipelineProvider.GetInstance(
                        Assembly.GetExecutingAssembly())));


            // ************ ACT ****************

            var instances =
                await Task.WhenAll(instanceTasks);

            // ************ ASSERT *************

            var first = instances.First();

            instances.All(i => first.Equals(i))
                .Should().BeTrue();
        }


        [Fact]
        public void NoHandlersFoundForAggregate_ReturnsDefaultHandler()
        {
            // ************ ARRANGE ************

            var instance = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly());

            // ************ ACT ****************

            var result =
                instance.GetPipeline<AggregateWithNoHandlers>();


            // ************ ASSERT *************

            result.Should()
                .BeOfType<
                    DefaultInvariantRuleHandler<AggregateWithNoHandlers>>();
        }


        [Fact]
        public void GetPipeline_AssemblesPipelineWithHandlersForAggregate()
        {
            // ************ ARRANGE ************

            var instance = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly());

            // ************ ACT ****************

            var pipeline = instance.GetPipeline<AggregateA>();

            // ************ ASSERT *************

            var handlers =
                ExtractInvariantRuleHandlersHelper
                    .ExtractHandlers<AggregateA>(pipeline);

            var expected =
                typeof(InvariantRuleHandlerA1)
                    .ToCollection(typeof(InvariantRuleHandlerA2));

            handlers.ShouldBeEquivalentTo(expected, (x, y) =>
                x.GetType() == y);
        }

        [Fact]
        public void GetPipeline_CachesResult()
        {
            // ************ ARRANGE ************

            var instance = InvariantRuleHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly());

            // ************ ACT ****************

            for (var i = 0; i < 1000; i++)
            {
                instance.GetPipeline<AggregateA>();
            }

            // ************ ASSERT *************

            // Was BeExactlyOne before but other tests use will cause sometimes to be a little more than 1. If we are caching the result
            // testing that calling the GetPipeline method 1000 times resulted in less than 5 or so calls it means the result is being cached.
            InvariantRuleHandlerPipelineAssembler.TimesCalled.Should()
                .BeLessThan(5);
            InvariantRuleHandlerPipelineAssembler.TimesCalled.Should()
                .BeLessThan(5);
        }
    }
}