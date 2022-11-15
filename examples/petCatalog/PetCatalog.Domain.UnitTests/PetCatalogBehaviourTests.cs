
using FluentAssertions;
using PetCatalogApp.Domain.Aggregates.Imp;
using PetCatalogApp.Domain.ValueObjects;

namespace PetCatalogApp.Domain.UnitTests
{
    public class PetCatalogBehaviourTests : PetCatalogTestsBase
    {
        [Fact]
        public void Constructor()
        {
            // ************ ARRANGE ************

            // ************ ACT ****************

            var sut = new PetCatalog(Id, "summer");

            // ************ ASSERT *************

            sut.Id.Should().Be(Id);
            sut.CatalogName.Should().Be("summer");

        }
        

        [Fact]
        public void AddPet()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var petId = new PetId();

            // ************ ACT ****************

            sut.AddPet(petId, "tiger");
           

            // ************ ASSERT *************

            var expected = new Tuple<PetId, string>(petId, "tiger");
            
            sut.Pets.Any(p=>p.Id == expected.Item1 && p.Name == expected.Item2)
                .Should().BeTrue();
        }


    }
}