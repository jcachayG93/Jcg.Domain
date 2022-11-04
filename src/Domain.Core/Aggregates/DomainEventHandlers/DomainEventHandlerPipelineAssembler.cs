using Domain.Core.Exceptions;

namespace Domain.Core.Aggregates.DomainEventHandlers
{
    /// <summary>
    /// Assembles the DomainEventHandlerPipeline for the specified aggregate type
    /// </summary>
    internal class DomainEventHandlerPipelineAssembler
    {
        /// <summary>
        /// Provided the handlerTypes are of type DomainEventHandlerBase[TAggregate] with
        /// parameterless constructors, will instantiate the handlers and assemble them
        /// into a domain event handler pipeline
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <param name="handlerTypes"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DomainEventHandlerBase<TAggregate>? AssemblePipelines<TAggregate>(
            IEnumerable<Type> handlerTypes)
        where TAggregate : AggregateRootBase
        {
            TimesCalled++;

            AssertAllHaveParameterlessConstructor(handlerTypes);

            var handlers = handlerTypes.Select(t =>
                Activator.CreateInstance(t)!)
                .Cast<DomainEventHandlerBase<TAggregate>>();

            if (!handlers.Any())
            {
                return null;
            }

            DomainEventHandlerBase<TAggregate>? firstHandler = null;
            DomainEventHandlerBase<TAggregate>? previousHandler = null;

            foreach (var handler in handlers)
            {
                if (firstHandler is null)
                {
                    firstHandler = handler;
                    previousHandler = firstHandler;
                }
                else
                {
                    previousHandler!.SetNext(handler);
                    previousHandler = handler;
                }
            }

            return firstHandler!;

        }

        private void AssertAllHaveParameterlessConstructor(IEnumerable<Type> handlerTypes)
        {
            foreach (var t in handlerTypes)
            {
                if (t.GetConstructor(Type.EmptyTypes) is null)
                {
                    throw new DomainEventHandlersParameterlessConstructorNotFoundException(
                        t.FullName!);
                }
            }
        }

        /// <summary>
        /// Just for testing, because I am not using a DI framework, this makes it much easier to test that the
        /// DomainEventHandlerPipeline provider caches the result
        /// </summary>
        internal static int TimesCalled { get; private set; }
    }
}
