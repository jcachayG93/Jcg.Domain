using FluentAssertions;
using PetCatalogApp.Domain.Aggregates.Imp;
using PetCatalogApp.Domain.Exceptions;
using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.UnitTests;

public class PetCatalogInvariantTEsts : PetCatalogTestsBase
{
    [Fact]
    public void RuleCatalogNameIsRequired()
    {
        // ************ ARRANGE ************

        // ************ ACT ****************

        var act = new Action(() =>
        {
            new PetCatalog(new CatalogId(), "");
        });

        // ************ ASSERT *************

        act.Should().Throw<CatalogNameIsBlankException>();
    }

    [Fact]
    public void RulePetNameIsRequired()
    {
        // ************ ARRANGE ************

        var sut = CreateSut();

        // ************ ACT ****************

        var act = new Action(() =>
        {
            sut.AddPet(new(), "");
        });

        // ************ ASSERT *************

        act.Should().Throw<PetNameIsBlankException>();
    }

    [Fact]
    public void RulePetIdMustBeUnique()
    {
        // ************ ARRANGE ************

        var petId = new PetId();

        var sut = CreateSut();

        sut.AddPet(petId, "aaa");

        // ************ ACT ****************

        var act = new Action(() =>
        {
            sut.AddPet(petId, "yyy");
        });

        // ************ ASSERT *************

        act.Should().Throw<PetIdAlreadyExistsException>();
    }
}