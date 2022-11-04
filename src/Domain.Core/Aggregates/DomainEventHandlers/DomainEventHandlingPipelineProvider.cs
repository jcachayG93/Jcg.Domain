using System.Reflection;

namespace Domain.Core.Aggregates.DomainEventHandlers
{
    internal class DomainEventHandlingPipelineProvider
    {
        private readonly Assembly _assemblyToScan;
        private static DomainEventHandlingPipelineProvider? _instance = null;
        private static readonly object LockObject = new object ();

        private DomainEventHandlingPipelineProvider(
            Assembly assemblyToScan)
        {
            _assemblyToScan = assemblyToScan;
            _scanner = new DomainEventHandlerTypesScanner();
            _assembler = new DomainEventHandlerPipelineAssembler();
        }

        public static DomainEventHandlingPipelineProvider GetInstance(Assembly assemblyToScan)
        {
           

            lock (LockObject)
            {
                if (_instance is null)
                {
                    _instance = new(assemblyToScan);
                }

                return _instance;
            }
        }

        public DomainEventHandlerBase<TAggregate>? GetPipeline<TAggregate>()
            where TAggregate : AggregateRootBase
        {
            // Shortcut with the cached result
            if (_cache.ContainsKey(typeof(TAggregate)))
            {
                return (DomainEventHandlerBase<TAggregate>?)_cache[typeof(TAggregate)];
            }

            // Here no data existed in the cache for this aggregate, so, add one
            var handlerTypes = _scanner.GetHandlerTypes<TAggregate>(_assemblyToScan);
            
            var pipeline = _assembler.AssemblePipelines<TAggregate>(handlerTypes);
            
            // We add the pipeline to the cache, even if it is null, so, we don't have to scan again next time this is called,
            // null indicates there are no handlers in the assembly.
            _cache.Add(typeof(TAggregate),pipeline);

            return pipeline;

        }

        /// <summary>
        /// Caches GetPipeline result, the key is the Aggregate type, the value is the Pipeline instance,
        /// is important to cache this data because it is expensive to calculate, that is why this class is a singleton.
        /// The pipeline instance can be null because there may be no handlers for the aggregate type. Still having a null object there
        /// avoids scanning the assembly again in the future.
        /// </summary>
        private Dictionary<Type, object?> _cache = new();

        private readonly DomainEventHandlerTypesScanner _scanner;
        private readonly DomainEventHandlerPipelineAssembler _assembler;
    }
}
