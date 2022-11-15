using Jcg.Domain.ValueObjects;

namespace PetCatalogApp.Domain.ValueObjects;

public record CatalogId : EntityIdentityBase
{
    public CatalogId(Guid id) : base(id)
    {
    }

    public CatalogId() : this(Guid.NewGuid())
    {

    }
}