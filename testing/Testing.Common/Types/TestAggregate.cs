using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.UnitTests.TestCommon
{
    public class TestAggregate
        : AggregateRootBase
    {
        protected override void When(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void AssertEntityStateIsValud()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Guid GetId()
        {
            throw new NotImplementedException();
        }
    }
}