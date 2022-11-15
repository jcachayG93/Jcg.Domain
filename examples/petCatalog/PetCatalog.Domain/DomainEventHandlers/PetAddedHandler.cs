using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Aggregates.DomainEvents;
using PetCatalogApp.Domain.Aggregates;
using PetCatalogApp.Domain.Aggregates.Imp;

namespace PetCatalogApp.Domain.DomainEventHandlers;

internal class PetAddedHandler : DomainEventHandlerBase<PetCatalog>
{
    protected override bool PerformHandling(PetCatalog aggregate, IDomainEvent domainEvent)
    {
        if (domainEvent is DomainEvents.PetAdded cev)
        {
            var pet = new Pet(new(cev.PetId), cev.PetName);

            aggregate.APets.Add(pet);
                
            return true;
        }

        return false;
    }
}