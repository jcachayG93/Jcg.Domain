using Jcg.Domain.Aggregates;
using Jcg.Domain.Aggregates.DomainEvents;

namespace Jcg.Domain.UnitTests.Types
{
    public class AggregateA : AggregateRootBase
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

        protected override Guid GetId()
        {
            throw new NotImplementedException();
        }
    }
}