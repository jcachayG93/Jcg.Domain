using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.Aggregates.Imp;

internal class Pet : IPet
{
    public Pet(PetId id, string name)
    {
        Id = id;
        Name = name;
    }

    public PetId Id { get; }
    public string Name { get; }
}