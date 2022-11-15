namespace Jcg.Domain.Aggregates.DomainEvents;

/// <summary>
///     A domain event that updates an aggregate but does not initialize it.
/// </summary>
public interface INonCreationalDomainEvent : IDomainEvent
{
}