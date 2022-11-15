using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.Aggregates;

public interface IPet
{
    PetId Id { get; }

    string Name { get; }

        
}