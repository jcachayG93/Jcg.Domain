using Domain.Core.Aggregates.Handlers;
using Domain.Core.Exceptions;
using Domain.Core.UnitTests.TestCommon;
using FluentAssertions;
using Testing.Common.Mocks;

namespace Domain.Core.UnitTests.Aggregates.Handlers
{
    public class DomainEventHandlerTests
    {
        private TestAggregate Aggregate { get; } = new();

        private DomainEventHandler<TestAggregate> CreateSutWithoutNextHandler(
            out DomainEventHandlerMock handlerStrategy)
        {
            handlerStrategy = new();

            return new(handlerStrategy.Object);
        }

        private DomainEventHandler<TestAggregate> CreateSutWithNextHandler(
            out DomainEventHandlerMock handlerStategy,
            out DomainEventHandlerMock nextHandler)
        {
            var sut = CreateSutWithoutNextHandler(out handlerStategy);

            nextHandler = new();

            sut.SetNext(nextHandler.Object);

            return sut;
        }


        [Fact]
        public void Handle_DelegatesToStrategy()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithoutNextHandler(
                out var strategy);

            var ev = RandomDomainEvent();

            strategy.SetupPerformHandlingResult(true);

            // ************ ACT ****************

            sut.Handle(Aggregate, ev);

            // ************ ASSERT *************

            strategy.VerifyPerformHandling(Aggregate, ev);
        }


        [Fact]
        public void StrategyHandlesEvent_DoesNotInvokeNext()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithNextHandler(
                out var strategy,
                out var next);

            var ev = RandomDomainEvent();

            strategy.SetupPerformHandlingResult(true);

            // ************ ACT ****************

            sut.Handle(Aggregate, ev);

            // ************ ASSERT *************

            next.VerifyNoOtherCalls();
        }


        [Fact]
        public void StrategyDidNotHandle_NextNotNull_DelegatesToNext()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithNextHandler(
                out var strategy,
                out var next);

            var ev = RandomDomainEvent();

            strategy.SetupPerformHandlingResult(false);

            // ************ ACT ****************

            sut.Handle(Aggregate, ev);

            // ************ ASSERT *************

            next.VerifyPerformHandling(Aggregate, ev);
        }


        [Fact]
        public void StrategyDidNotHandle_NextIsNull_ThrowsException()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithoutNextHandler(
                out var strategy);

            var ev = RandomDomainEvent();

            strategy.SetupPerformHandlingResult(false);

            // ************ ACT ****************

            var act = () => { sut.Handle(Aggregate, ev); };

            // ************ ASSERT *************

            act.Should().Throw<UnhandledDomainEventException>();
        }
    }
}