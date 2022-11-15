using FluentAssertions;
using Jcg.Domain.Exceptions;
using Jcg.Domain.ValueObjects;

namespace Jcg.Domain.UnitTests.ValueObjects
{
    public class EntityIdentityBaseTests
    {


        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void Constructor_IdIsEmpty_ThrowsException(
            bool idIsEmpty, bool shouldThrow)
        {

            // ************ ARRANGE ************

            // ************ ACT ****************

            Action act = () =>
            {
                new TestId(idIsEmpty ? Guid.Empty : Guid.NewGuid());
            };

            // ************ ASSERT *************

            if (shouldThrow)
            {
                act.Should().Throw<EntityIdentityValueIsEmptyException>();
            }
            else
            {
                act.Should().NotThrow();
            }
        }


        [Fact]
        public void Constructor_SetsIdValue()
        {


            // ************ ARRANGE ************

            var id = Guid.NewGuid();

            // ************ ACT ****************

            var result = new TestId(id);

            // ************ ASSERT *************

            result.Id.Should().Be(id);

        }

        record TestId : EntityIdentityBase
        {
            /// <inheritdoc />
            public TestId(Guid id) : base(id)
            {
            }
        }
    }
}
