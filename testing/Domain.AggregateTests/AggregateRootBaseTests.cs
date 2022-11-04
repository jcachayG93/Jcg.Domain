using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.AggregateTests.Types;
using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.Exceptions;
using FluentAssertions;
using Testing.Common.Assertions;
using Testing.Common.Extensions;

namespace Domain.Core.UnitTests.Aggregates
{
    public class AggregateRootBaseTests
    {
        private AggregateDouble CreateSut()
        {
            return new(Guid.NewGuid(), Assembly.GetExecutingAssembly());
        }

        private CreationalDomainEvent CreateCreationalEvent()
        {
            return new(Guid.NewGuid());
        }

        private NonCreationalDomainEvent CreateNonCreationalDomainEvent(Guid aggregateId)
        {
            return new(aggregateId);
        }

        [Theory]
        [InlineData(false,false,false)]
        [InlineData(true,false,true)]
        [InlineData(true,true,false)]
        public void Apply_EventIsNoCreational_AggregateIdDoesNotMatch_ThrowsException(
            bool isNonCreational, bool idMatches, bool shouldThrow)
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var eventAggregateId = idMatches ? sut.Id : Guid.NewGuid();

            IDomainEvent ev = isNonCreational
                ? CreateNonCreationalDomainEvent(eventAggregateId)
                : CreateCreationalEvent();

            // ************ ACT ****************

            var act = new Action(() =>
            {
                sut.Apply(ev);
            });

            // ************ ASSERT *************

            if (shouldThrow)
            {
                act.Should().Throw<AggregateIdDoesNotMatchForNonCreationalEventException>();
            }
            else
            {
                act.Should().NotThrow();
            }
        }



        [Fact]
        public void Apply_DelegatesToWhen()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var ev = CreateCreationalEvent();

            // ************ ACT ****************

            sut.Apply(ev);

            // ************ ASSERT *************

            sut.Test_WhenArgs.Should().Be(ev);
        }

        [Fact]
        public void Apply_IncrementsVersion()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var ev = CreateCreationalEvent();

            var versionBefore = sut.Version;

            // ************ ACT ****************

            sut.Apply(ev);

            // ************ ASSERT *************

            sut.Version.Should().Be(versionBefore+1);
        }

        [Fact]
        public void Apply_AddsDomainEventToChanges()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var ev = CreateCreationalEvent();

            // ************ ACT  ****************

            sut.Apply(ev);

            // ************ ASSERT *************

            sut.Changes.ShouldBeEquivalentTo(ev.ToCollection());
        }

        [Fact]
        public void ResetChanges_DoesThat()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var ev = CreateCreationalEvent();

            sut.Apply(ev);

            sut.Changes.Any().Should().BeTrue();

            // ************ ACT ****************

            sut.ResetChanges();

            // ************ ASSERT *************

            sut.Changes.Should().BeEmpty();
        }


    }
}
