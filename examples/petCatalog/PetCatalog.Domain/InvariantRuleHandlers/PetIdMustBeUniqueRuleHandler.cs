using Domain.Core.Aggregates.InvarianRuleHandlers;
using PetCatalogApp.Domain.Aggregates.Imp;
using PetCatalogApp.Domain.Exceptions;

namespace PetCatalogApp.Domain.InvariantRuleHandlers;

internal class PetIdMustBeUniqueRuleHandler : InvariantRuleHandlerBase<PetCatalog>
{
    protected override void AssertEntityStateIsValid(PetCatalog aggregate)
    {
        if (AtLeastTwoPetsHaveTheSameId(aggregate))
        {
            throw new PetIdAlreadyExistsException();
        }
    }

    private bool AtLeastTwoPetsHaveTheSameId(PetCatalog aggregate)
    {
        return aggregate.APets.DistinctBy(p => p.Id).Count() < aggregate.APets.Count();
    }
}