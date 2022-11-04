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

        private CreationalDomainEvent CreateCreationalEvent(Guid aggregateId, string name = "")
        {
            return new(aggregateId, name);
        }

        private NonCreationalDomainEvent CreateNonCreationalDomainEvent(Guid aggregateId,
            string name = "")
        {
            return new(aggregateId, name);
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
                : CreateCreationalEvent(eventAggregateId);

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
        public void Apply_NoHandlers_ThrowsException()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var ev = new EventWithNoHandlers(Guid.NewGuid());

            // ************ ACT ****************

            var act = new Action(() =>
            {
                sut.Apply(ev);
            });

            // ************ ASSERT *************

            act.Should().Throw<UnhandledDomainEventException>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Apply_DelegatesToDomainEventHandlerPipeline(
            bool isCreational)
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            IDomainEvent ev = isCreational
                ? CreateCreationalEvent(sut.Id, "aaa")
                : CreateNonCreationalDomainEvent(sut.Id, "aaa");

            // ************ ACT ****************

            sut.Apply(ev);

            // ************ ASSERT *************

            sut.Name.Should().Be("aaa");
        }

        [Fact]
        public void Apply_IncrementsVersion()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var ev = CreateCreationalEvent(sut.Id, "");

            var versionBefore = sut.Version;

            // ************ ACT ****************

            sut.Apply(ev);

            // ************ ASSERT *************

            sut.Version.Should().Be(versionBefore++);
        }

        [Fact]
        public void Apply_AddsDomainEventToChanges()
        {
            // ************ ARRANGE ************

            var sut = CreateSut();

            var ev = CreateCreationalEvent(sut.Id, "");

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

            var ev = CreateCreationalEvent(sut.Id, "");

            sut.Apply(ev);

            // ************ ACT ****************

            sut.ResetChanges();

            // ************ ASSERT *************

            sut.Changes.Should().BeEmpty();
        }


    }
}
