using Domain.IntegrationTests.Aggregate;
using FluentAssertions;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.IntegrationTests
{
    public class CustomerTests
    {
        private Customer CreateSut()
        {
            return new(Guid.NewGuid(), "aaa");
        }

        [Fact]
        public void CreatesCustomer()
        {
            // ************ ARRANGE ************

            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // ************ ACT ****************

            var sut = new Customer(id, name);

            // ************ ASSERT *************

            sut.Id.Should().Be(id);

            sut.Name.Should().Be(name);
        }


        [Fact]
        public void UpdatesCustomerName()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            // ************ ACT ****************

            sut.Update("zzz");

            // ************ ASSERT *************

            sut.Name.Should().Be("zzz");
        }


        [Fact]
        public void AddsOrders()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var orderId = Guid.NewGuid();

            // ************ ACT ****************

            sut.AddOrder(orderId);

            // ************ ASSERT *************

            sut.Orders.ShouldBeEquivalentTo(orderId.ToCollection(), (x, y) =>
                x.Id == y);
        }


        [Fact]
        public void InvariantRule_CustomerCantHaveMoreThan3Orders()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            sut.AddOrder(Guid.NewGuid());
            sut.AddOrder(Guid.NewGuid());
            sut.AddOrder(Guid.NewGuid());

            // ************ ACT ****************

            var act = () => { sut.AddOrder(Guid.NewGuid()); };

            // ************ ASSERT *************

            act.Should().Throw<CustomerHasMoreThanThreeOrdersException>();
        }
    }
}