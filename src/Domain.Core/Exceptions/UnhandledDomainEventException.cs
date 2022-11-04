using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.Exceptions
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