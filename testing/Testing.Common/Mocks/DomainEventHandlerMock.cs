using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Aggregates.Handlers;
using Domain.Core.UnitTests.TestCommon;
using Moq;

namespace Testing.Common.Mocks
{
    public class DomainEventHandlerMock
    {
        public DomainEventHandlerMock()
        {
            _moq = new();

            SetupPerformHandlingResult(false);
        }

        public IDomainEventHandler<TestAggregate> Object => _moq.Object;

        public void VerifyPerformHandling(
            TestAggregate aggregate, IDomainEvent domainEvent)
        {
            _moq.Verify(s =>
                s.PerformHandling(aggregate, domainEvent));
        }

        public void SetupPerformHandlingResult(bool returns)
        {
            _moq.Setup(s =>
                    s.PerformHandling(It.IsAny<TestAggregate>(),
                        It.IsAny<IDomainEvent>()))
                .Returns(returns);
        }

        public void VerifyNoOtherCalls()
        {
            _moq.VerifyNoOtherCalls();
        }

        private readonly Mock<IDomainEventHandler<TestAggregate>> _moq;
    }
}