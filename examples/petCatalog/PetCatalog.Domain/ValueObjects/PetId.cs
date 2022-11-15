using Jcg.Domain.ValueObjects;

namespace PetCatalogApp.Domain.ValueObjects
{
    public record PetId : EntityIdentityBase
    {
        public PetId(Guid id) : base(id)
        {
        }

        public PetId() : this(Guid.NewGuid())
        {

        }
    }
}
