using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.Aggregates
{
    public interface IPetCatalog
    {
        CatalogId Id { get; }

        string CatalogName { get; }

        IReadOnlyCollection<IPet> Pets { get; }

        void AddPet(PetId petId, string name);

    }
}
