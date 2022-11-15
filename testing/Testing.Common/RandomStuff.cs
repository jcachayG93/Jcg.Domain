using Jcg.Domain.Aggregates.DomainEvents;
using Moq;

namespace Testing.Common
{
    public static class RandomStuff
    {
        public static IDomainEvent RandomDomainEvent()
        {
            return Mock.Of<IDomainEvent>();
        }
    }
}