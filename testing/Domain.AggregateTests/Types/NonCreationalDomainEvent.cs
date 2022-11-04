using Domain.Core.Aggregates.DomainEvents;

namespace Domain.AggregateTests.Types;

/// <summary>
/// For the tests, this event will update the Aggregate's Name property
/// </summary>
public record NonCreationalDomainEvent(Guid AggregateId, string Name) : INonCreationalDomainEvent;