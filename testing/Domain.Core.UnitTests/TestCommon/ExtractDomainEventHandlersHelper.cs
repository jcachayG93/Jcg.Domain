using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.UnitTests.Types;

namespace Domain.Core.UnitTests.TestCommon;

public static class ExtractDomainEventHandlersHelper
{
    /// <summary>
    /// Extracts all the handlers from the pipeline
    /// </summary>
    public static IEnumerable<DomainEventHandlerBase<TAggregate>> ExtractHandlers<TAggregate>(
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