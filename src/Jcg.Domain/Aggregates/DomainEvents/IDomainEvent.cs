namespace Jcg.Domain.Aggregates.DomainEvents
{
    /// <summary>
    /// A domain event, describing how to update the state of an aggregate
    /// </summary>
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}
