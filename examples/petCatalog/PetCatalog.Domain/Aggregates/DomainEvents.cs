using Jcg.Domain.Aggregates.DomainEvents;

namespace PetCatalogApp.Domain.Aggregates
{
    public static class DomainEvents
    {
        public record PetCatalogCreated(Guid AggregateId, string Name) : ICreationalDomainEvent;

        public record PetAdded(Guid AggregateId, Guid PetId, string PetName) : INonCreationalDomainEvent;
    }
}
