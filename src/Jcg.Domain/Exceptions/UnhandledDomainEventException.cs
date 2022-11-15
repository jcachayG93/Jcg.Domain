using Jcg.Domain.Aggregates.DomainEvents;

namespace Jcg.Domain.Exceptions
{
    public class UnhandledDomainEventException
        : DomainCoreException
    {
        public UnhandledDomainEventException(
            IDomainEvent domainEvent)
            : base($"A handler for the domain event of type" +
                   $"{domainEvent.GetType().ToString()} was not found in the assembly")
        {
        }
    }
}