using Jcg.Domain.Aggregates;
using Jcg.Domain.Aggregates.DomainEventHandlers;

namespace Jcg.Domain.UnitTests.TestCommon;

public static class ExtractDomainEventHandlersHelper
{
    /// <summary>
    ///     Extracts all the handlers from the pipeline
    /// </summary>
    public static IEnumerable<DomainEventHandlerBase<TAggregate>>
        ExtractHandlers<TAggregate>(
            DomainEventHandlerBase<TAggregate> pipeline)
        where TAggregate : AggregateRootBase
    {
        var result = new List<DomainEventHandlerBase<TAggregate>>();

        var current = pipeline;

        while (current.NextHandler != null)
        {
            result.Add(current);
            current = current.NextHandler;
        }

        result.Add(current);

        return result;
    }
}