namespace Domain.Core.Aggregates.Handlers;

public interface IInvariantRuleHandler<in TAggregate>
    where TAggregate : AggregateRootBase
{
    /// <summary>
    ///     Throws an exception if the aggregate is in an invalid state
    /// </summary>
    /// <param name="aggregate"></param>
    void AssertEntityStateIsValid(TAggregate aggregate);
}