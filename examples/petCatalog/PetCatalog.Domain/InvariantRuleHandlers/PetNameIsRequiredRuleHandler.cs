using Domain.Core.Aggregates.InvarianRuleHandlers;
using PetCatalogApp.Domain.Aggregates.Imp;
using PetCatalogApp.Domain.Exceptions;

namespace PetCatalogApp.Domain.InvariantRuleHandlers;

internal class PetNameIsRequiredRuleHandler : InvariantRuleHandlerBase<PetCatalog>
{
    protected override void AssertEntityStateIsValid(PetCatalog aggregate)
    {
        if (aggregate.APets.Any(p => string.IsNullOrWhiteSpace(p.Name)))
        {
            throw new PetNameIsBlankException();
        }
    }
}