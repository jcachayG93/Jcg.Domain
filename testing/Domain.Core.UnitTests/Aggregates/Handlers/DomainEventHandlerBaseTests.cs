using Domain.Core.Exceptions;
using Domain.Core.UnitTests.TestCommon;
using FluentAssertions;
using Testing.Common.Mocks;

namespace Domain.Core.UnitTests.Aggregates.Handlers
{
    public class DomainEventHandlerBaseTests
    {
        private TestAggregate Aggregate { get; } = new();

        private DomainEventHandlerMock CreateSut()
        {
            return new();
        }

        private DomainEventHandlerMock CreateSutWithNext(
            out DomainEventHandlerMock next)
        {
            next = new();

            var result = CreateSut();

            result.SetNext(next);

            return result;
        }

        [Fact]
        public void Handle_DelegatesToPerformHandling()
        {
            // ************ ARRANGE ************

            var ev = RandomDomainEvent();

            var sut = CreateSut();

            sut.SetupSoEventIsHandled();

            // ************ ACT ****************

            sut.Handle(Aggregate, ev);

            // ************ ASSERT *************

            sut.PerformHandlingArgs
                .Should().Be(new PerformHandlingArgs(Aggregate, ev));
        }


        [Fact]
        public void EventWasHandled_DoesNotInvokeNext()
        {
            // ************ ARRANGE ************

            var ev = RandomDomainEvent();

            var sut = CreateSutWithNext(out var next);

            sut.SetupSoEventIsHandled();

            // ************ ACT ****************

            sut.Handle(Aggregate, ev);

            // ************ ASSERT *************

            next.VerifyNoOtherCalls();
        }


        [Fact]
        public void WasNotHandled_NextNotNull_DelegatesToNext()
        {
            // ************ ARRANGE ************

            var ev = RandomDomainEvent();

            var sut = CreateSutWithNext(out var next);

            sut.Test_WasHandledValue = false;

            // ************ ACT ****************

            sut.Handle(Aggregate, ev);

            // ************ ASSERT *************

            next.VerifyHandle(Aggregate, ev);
        }


        [Fact]
        public void WasNotHandled_NextNull_ThrowsException()
        {
            // ************ ARRANGE ************

            var ev = RandomDomainEvent();

            var sut = CreateSut();

            sut.Test_WasHandledValue = false;

            // ************ ACT ****************

            var act = () => { sut.Handle(Aggregate, ev); };

            // ************ ASSERT *************

            act.Should().Throw<UnhandledDomainEventException>();
        }
    }
}