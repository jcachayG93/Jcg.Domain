using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Aggregates.Handlers;
using Domain.Core.UnitTests.TestCommon;
using Moq;

namespace Testing.Common.Mocks
{
    [Obsolete]
// TODO: R200 Remove
    public class DomainEventHandlerBaseMock
    {
        public DomainEventHandlerBaseMock()
        {
            _moq = new();
        }

        public DomainEventHandlerBase<TestAggregate> Object => _moq.Object;

        public void VerifyHandle(
            TestAggregate aggregate, IDomainEvent domainEvent)
        {
            _moq.Verify(s =>
                s.Handle(aggregate, domainEvent));
        }

        public void VerifyNoOtherCalls()
        {
            _moq.VerifyNoOtherCalls();
        }

        private readonly Mock<DomainEventHandlerBase<TestAggregate>> _moq;
    }

    public class DomainEventHandlerMock : DomainEventHandlerBase<TestAggregate>
    {
        public PerformHandlingArgs? PerformHandlingArgs { get; set; } =
            null;

        public void SetupSoEventIsHandled()
        {
            _wasHandledValue = true;
        }

        public void SetupSoEventIsNotHandled()
        {
            _wasHandledValue = false;
        }

        /// <inheritdoc />
        protected override void PerformHandling(
            TestAggregate aggregate, IDomainEvent domainEvent,
            out bool wasHandled)
        {
            PerformHandlingArgs = new(aggregate, domainEvent);

            wasHandled = _wasHandledValue;
        }


        private bool _wasHandledValue = true;
    }

    public record PerformHandlingArgs(
        TestAggregate Aggregate, IDomainEvent DomainEvent);
}