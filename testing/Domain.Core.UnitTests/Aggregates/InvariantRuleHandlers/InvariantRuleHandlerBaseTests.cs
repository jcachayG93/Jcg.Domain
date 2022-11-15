using FluentAssertions;
using Jcg.Domain.Aggregates.InvarianRuleHandlers;
using Testing.Common.Mocks;
using Testing.Common.Types;

namespace Jcg.Domain.UnitTests.Aggregates.InvariantRuleHandlers
{
    public class InvariantRuleHandlerBaseTests
    {
        private TestAggregate Aggregate { get; } = new();

        private SutDouble CreateSutWithoutNext(
            bool throwsException = false)
        {
            var result = new SutDouble();

            result.MakeAssertEntityStateIsValidThrowException = throwsException;

            return result;
        }

        private SutDouble CreateSutWithNext(
            bool throwsException,
            out InvariantRuleHandlerBaseMock<TestAggregate> nextHandler)
        {
            var sut = CreateSutWithoutNext(throwsException);

            nextHandler = new();

            sut.SetNext(nextHandler);

            return sut;
        }


        [Fact]
        public void Handle_DelegatesToAssertEntityStateIsValid()
        {
            // ************ ARRANGE ************

            var sut =
                CreateSutWithoutNext();

            // ************ ACT ****************

            sut.Handle(Aggregate);

            // ************ ASSERT *************

            sut.AssertEntityStateIsValidArgs.Should().Be(Aggregate);
        }


        [Fact]
        public void
            AssertEntityStateIsValidDoesNotThrow_NextNull_DoesNotThrowAnyException()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithoutNext(false);

            // ************ ACT ****************

            var act = () => { sut.Handle(Aggregate); };

            // ************ ASSERT *************

            act.Should().NotThrow();
        }


        [Fact]
        public void
            AssertEntityStateIsValidDoesNotThrow_NextNotNull_DelegatesToNext()
        {
            // ************ ARRANGE ************

            var sut = CreateSutWithNext(false, out var next);

            // ************ ACT ****************

            sut.Handle(Aggregate);

            // ************ ASSERT *************

            next.HandleArgs.Should().Be(Aggregate);
        }

        private class SutDouble : InvariantRuleHandlerBase<TestAggregate>
        {
            public TestAggregate? AssertEntityStateIsValidArgs
            {
                get;
                private set;
            } = null;

            public bool MakeAssertEntityStateIsValidThrowException { get; set; }
                = false;

            /// <inheritdoc />
            protected override void AssertEntityStateIsValid(
                TestAggregate aggregate)
            {
                AssertEntityStateIsValidArgs = aggregate;

                if (MakeAssertEntityStateIsValidThrowException)
                {
                    throw new Exception();
                }
            }
        }
    }
}