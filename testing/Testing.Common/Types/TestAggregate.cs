using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEvents;

namespace Testing.Common.Types
{
    public class TestAggregate
        : AggregateRootBase
    {
        protected override void When(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void AssertEntityStateIsValid()
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