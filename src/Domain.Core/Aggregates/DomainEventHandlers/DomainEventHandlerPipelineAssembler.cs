using Domain.Core.Exceptions;

namespace Domain.Core.Aggregates.DomainEventHandlers
{
    /// <summary>
    ///     Assembles the DomainEventHandlerPipeline for the specified aggregate type
    /// </summary>
    internal class DomainEventHandlerPipelineAssembler
    {
        /// <summary>
        ///     Just for testing, because I am not using a DI framework, this makes it much easier to test that the
        ///     DomainEventHandlerPipeline provider caches the result
        /// </summary>
        internal static int TimesCalled { get; private set; }

        /// <summary>
        ///     Provided the handlerTypes are of type DomainEventHandlerBase[TAggregate] with
        ///     parameterless constructors, will instantiate the handlers and assemble them
        ///     into a domain event handler pipeline
        /// </summary>
        /// <typeparam name="TAggregate">The aggregate type</typeparam>
        /// <param name="handlerTypes">The handlers matching the specified aggregate type</param>
        /// <returns>The pipeline</returns>
        /// <exception cref="DomainEventHandlerParameterlessConstructorNotFoundException">
        ///     When domain event handler count not be instantiated because it did not have a parameterless constructor
        /// </exception>
        public DomainEventHandlerBase<TAggregate>
            AssemblePipeline<TAggregate>(
                IEnumerable<Type> handlerTypes)
            where TAggregate : AggregateRootBase
        {
            TimesCalled++;

            AssertAllHaveParameterlessConstructor(handlerTypes);

            var handlers =
                handlerTypes.Select(t =>
                        Activator.CreateInstance(t)!)
                    .Cast<DomainEventHandlerBase<TAggregate>>();


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

        private void AssertAllHaveParameterlessConstructor(
            IEnumerable<Type> handlerTypes)
        {
            foreach (var t in handlerTypes)
            {
                if (t.GetConstructor(Type.EmptyTypes) is null)
                {
                    throw new
                        DomainEventHandlerParameterlessConstructorNotFoundException(
                            t.FullName!);
                }
            }
        }
    }
}