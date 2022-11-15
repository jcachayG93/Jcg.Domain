using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Exceptions;
using Domain.Core.UnitTests.TestCommon;
using FluentAssertions;
using Moq;
using Testing.Common.Mocks.DomainEventHandler;
using Testing.Common.Types;

namespace Domain.Core.UnitTests.Aggregates.DomainEventHandlers
{
    public class DomainEventHandlerBaseTests
    {
        private SutDouble CreateSutWithoutNext(
            bool performHandlingReturns = false)
        {
            var result = new SutDouble();

            result.PerformHandlingReturns = performHandlingReturns;

            return result;
        }

        private SutDouble CreateSutWithNext(
            bool performHandlingReturns, out DomainEventHandlerMock<TestAggregate> nextHandler)
        {
            var sut = CreateSutWithoutNext(performHandlingReturns);

            nextHandler = new();

            sut.SetNext(nextHandler);

            return sut;
        }

        private TestAggregate Aggregate { get; } = new();

        private IDomainEvent DomainEvent { get; } = Mock.Of<IDomainEvent>();

        [Fact]
        public void Handle_DelegatesToPerformHandling()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithoutNext(true);

            // ************ ACT ****************

            sut.Handle(Aggregate, DomainEvent);

            // ************ ASSERT *************

            sut.PerformHandlingArgs.Should().Be(new PerformHandlingArgs(Aggregate, DomainEvent));
        }

        [Fact]
        public void PerformHandlingReturnsTrue_ShortCutsPipelineByNotInvokingNextHandler()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithNext(true, 
                out var next);

            // ************ ACT ****************

            sut.Handle(Aggregate, DomainEvent);

            // ************ ASSERT *************

            next.HandleArgs.Should().BeNull();
        }

        [Fact]
        public void PerformHandlingReturnsFalse_NextIsNull_ThrowsException()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithoutNext(false);

            // ************ ACT ****************

            var act = new Action(() =>
            {
                sut.Handle(Aggregate, DomainEvent);
            });

            // ************ ASSERT *************

            act.Should().Throw<UnhandledDomainEventException>();
        }

        [Fact]
        public void PerformHandlingReturnsFalse_NextNotNull_DelegatesToNext()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithNext(false, out var next);

            // ************ ACT ****************

            sut.Handle(Aggregate, DomainEvent);

            // ************ ASSERT *************

            next.HandleArgs
                .Should().Be(new DomainEventHandlerHandleArgs<TestAggregate>(Aggregate, DomainEvent));
        }

        class SutDouble : DomainEventHandlerBase<TestAggregate>
        {
            public PerformHandlingArgs? PerformHandlingArgs { get; private set; } = null;

            public bool PerformHandlingReturns { get; set; }
            protected override bool PerformHandling(TestAggregate aggregate, IDomainEvent domainEvent)
            {
                PerformHandlingArgs = new(aggregate, domainEvent);

                return PerformHandlingReturns;
            }
        }

        record PerformHandlingArgs(TestAggregate Aggregate, IDomainEvent DomainEvent);
    }
}
