using Jcg.Domain.Aggregates.DomainEvents;

namespace Domain.AggregateTests.Types;

/// <summary>
/// For the tests, this event will update the Aggregate's Name property
/// </summary>
public record CreationalDomainEvent(Guid AggregateId) : ICreationalDomainEvent;