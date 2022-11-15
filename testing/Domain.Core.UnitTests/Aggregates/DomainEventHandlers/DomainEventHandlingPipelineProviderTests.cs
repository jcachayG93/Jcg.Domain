using System.Reflection;
using FluentAssertions;
using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Exceptions;
using Jcg.Domain.UnitTests.TestCommon;
using Jcg.Domain.UnitTests.Types;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Jcg.Domain.UnitTests.Aggregates.DomainEventHandlers
{
    public class DomainEventHandlingPipelineProviderTests
    {
        [Fact]
        public async Task ImplementsSingletonPattern()
        {
            /*
             * Get many instances in parallel, then assert all are the same object
             */

            // ************ ARRANGE ************

            var instanceTasks = Enumerable.Range(0, 500000)
                .Select(i => Task.Run(() =>
                    DomainEventHandlingPipelineProvider.GetInstance(
                        Assembly.GetExecutingAssembly())));


            // ************ ACT ****************

            var instances = await Task.WhenAll(instanceTasks);

            // ************ ASSERT *************

            var first = instances.First();

            instances.All(i => first.Equals(i))
                .Should().BeTrue();
        }


        [Fact]
        public void NoHandlersFoundForAggregate_ThrowsException()
        {
            // ************ ARRANGE ************

            var instance = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly());

            // ************ ACT ****************

            var act = () =>
            {
                instance.GetPipeline<AggregateWithNoHandlers>();
            };

            // ************ ASSERT *************

            act
                .Should().Throw<NoDomainEventHandlersFoundException>();
        }

        [Fact]
        public void GetPipeline_AssemblesPipelineWithHandlersForAggregate()
        {
            // ************ ARRANGE ************

            var instance = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly());

            // ************ ACT ****************

            var pipeline = instance.GetPipeline<AggregateB>();

            // ************ ASSERT *************

            var handlers = ExtractDomainEventHandlersHelper
                .ExtractHandlers<AggregateB>(pipeline);

            var expected =
                typeof(DomainEventHandlerB1)
                    .ToCollection(typeof(DomainEventHandlerB2));

            handlers.ShouldBeEquivalentTo(expected, (x, y) =>
                x.GetType() == y);
        }

        [Fact]
        public void GetPipeline_CachesResult()
        {
            // ************ ARRANGE ************

            var instance = DomainEventHandlingPipelineProvider
                .GetInstance(Assembly.GetExecutingAssembly());

            // ************ ACT ****************

            for (var i = 0; i < 1000; i++)
            {
                instance.GetPipeline<AggregateB>();
            }

            // ************ ASSERT *************

            // Was BeExactlyOne before but other tests use will cause sometimes to be a little more than 1. If we are caching the result
            // testing that calling the GetPipeline method 1000 times resulted in less than 5 or so calls it means the result is being cached.
            DomainEventHandlerTypesScanner.TimesCalled.Should().BeLessThan(5);
            DomainEventHandlerPipelineAssembler.TimesCalled.Should()
                .BeLessThan(5);
        }
    }
}