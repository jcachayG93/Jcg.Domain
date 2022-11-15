using System.Collections.Concurrent;
using System.Reflection;

namespace Jcg.Domain.Aggregates.InvarianRuleHandlers
{
    public class InvariantRuleHandlingPipelineProvider
    {
        private InvariantRuleHandlingPipelineProvider(Assembly assemblyToScan)
        {
            _assemblyToScan = assemblyToScan;
            _scanner = new InvariantRuleHandlerTypesScanner();
            _assembler = new InvariantRuleHandlerPipelineAssembler();
        }

        public static InvariantRuleHandlingPipelineProvider GetInstance(
            Assembly assemblyToScan)
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

        public InvariantRuleHandlerBase<TAggregate> GetPipeline<TAggregate>()
            where TAggregate : AggregateRootBase
        {
            // Shortcut with the cached result
            if (_cache.ContainsKey(typeof(TAggregate)))
            {
                return (InvariantRuleHandlerBase<TAggregate>)_cache[
                    typeof(TAggregate)];
            }

            // Here no data existed in the cache for this aggregate, so, add one
            var handlerTypes =
                _scanner.GetHandlerTypes<TAggregate>(_assemblyToScan);

            if (!handlerTypes.Any())
            {
                /*
                 * For the invariant rules, there may be an aggregate with no invariants,
                 * we dont want to return null, but instead a single handler
                 * that never throws an
                 * exception
                 */
                handlerTypes =
                    new List<Type>()
                    {
                        typeof(DefaultInvariantRuleHandler<TAggregate>)
                    };
            }

            var pipeline = _assembler
                .AssemblePipeline<TAggregate>(handlerTypes)!;

            _cache.AddOrUpdate(typeof(TAggregate), pipeline,
                (k, v) => v);

            return pipeline;
        }

        private static InvariantRuleHandlingPipelineProvider? _instance = null;
        private static readonly object LockObject = new object();
        private readonly InvariantRuleHandlerPipelineAssembler _assembler;
        private readonly Assembly _assemblyToScan;

        private readonly ConcurrentDictionary<Type, object> _cache = new();
        private readonly InvariantRuleHandlerTypesScanner _scanner;
    }
}