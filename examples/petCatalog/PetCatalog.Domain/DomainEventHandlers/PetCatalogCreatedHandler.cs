using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Jcg.Domain.Aggregates.DomainEventHandlers;
using Jcg.Domain.Aggregates.DomainEvents;
using PetCatalogApp.Domain.Aggregates;
using PetCatalogApp.Domain.Aggregates.Imp;
using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.DomainEventHandlers
{
    internal class PetCatalogCreatedHandler : DomainEventHandlerBase<PetCatalog>
    {
        protected override bool PerformHandling(PetCatalog aggregate, IDomainEvent domainEvent)
        {
            if (domainEvent is DomainEvents.PetCatalogCreated cev)
            {
                aggregate.Id = new CatalogId(cev.AggregateId);
                aggregate.CatalogName = cev.Name;

                return true;
            }

            return false;
        }
    }
}
