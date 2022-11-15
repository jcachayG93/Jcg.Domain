using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates.InvarianRuleHandlers;
using PetCatalogApp.Domain.Aggregates.Imp;
using PetCatalogApp.Domain.Exceptions;

namespace PetCatalogApp.Domain.InvariantRuleHandlers
{
    internal class CatalogNameIsRequiredRuleHandler : InvariantRuleHandlerBase<PetCatalog>
    {
        protected override void AssertEntityStateIsValid(PetCatalog aggregate)
        {
            if (string.IsNullOrWhiteSpace(aggregate.CatalogName))
            {
                throw new CatalogNameIsBlankException();
            }
        }
    }
}
