using PetCatalogApp.Domain.Aggregates.Imp;
using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.UnitTests;

public class PetCatalogTestsBase
{
    internal PetCatalog CreateSut()
    {
      

        return new(Id, "aaa");
    }

    protected CatalogId Id { get; } = new();
}