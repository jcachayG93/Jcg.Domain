namespace Domain.Core.Aggregates.Handlers.Scanner
{
    internal class DomainEventHandlerPipelineAssembler
    {
        public IDomainEventHandlerPipeline<TAggregate>? Create<TAggregate>(
            IEnumerable<Type> handlersTypes)
            where TAggregate : AggregateRootBase
        {
            var handlers = handlersTypes
                .Select(h => Activator.CreateInstance(h)!)
                .Cast<IDomainEventHandler<TAggregate>>()
                .ToList();

            if (!handlers.Any())
            {
                return null;
            }

            DomainEventHandler<TAggregate>? first = null;

            DomainEventHandler<TAggregate>? current = null;

            foreach (var handler in handlers)
            {
                if (first is null)
                {
                    first = new(handler);

                    current = first;
                }
                else
                {
                    current!.SetNext(handler);
                }
            }

            return first;
        }
    }
}