using Jcg.Domain.Aggregates.DomainEvents;

namespace Domain.IntegrationTests.Aggregate;

public static class DomainEvents
{
    public record CustomerCreated
        (Guid AggregateId, string Name) : ICreationalDomainEvent;

    public record CustomerNameUpdated
        (Guid AggregateId, string Name) : INonCreationalDomainEvent;

    public record OrderAdded
        (Guid AggregateId, Guid OrderId) : INonCreationalDomainEvent;
}