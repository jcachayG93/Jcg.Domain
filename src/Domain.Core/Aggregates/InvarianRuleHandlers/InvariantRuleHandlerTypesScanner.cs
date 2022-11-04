using System.Reflection;

namespace Domain.Core.Aggregates.InvarianRuleHandlers
{
    /// <summary>
    ///     Scans the assembly to get all handler types for the specified aggregate type
    /// </summary>
    public class InvariantRuleHandlerTypesScanner
    {
        /// <summary>
        ///     Just for testing, because I am not using a DI framework, this makes it much easier to test that the
        ///     DomainEventHandlerPipeline provider caches the result
        /// </summary>
        internal static int TimesCalled { get; private set; }

        /// <summary>
        ///     Returns all types that are DomainEventHandlers for the specified aggregate type that exist in the specified
        ///     assembly
        /// </summary>
        /// <typeparam name="TAggregate">The aggregate type</typeparam>
        /// <param name="assemblyToScan">The assembly where to search for the handlers</param>
        /// <returns>The handler types</returns>
        public IEnumerable<Type> GetHandlerTypes<TAggregate>(
            Assembly assemblyToScan)
        {
            TimesCalled++;

            return
                assemblyToScan
                    .GetTypes()
                    .Where(t =>
                        t.BaseType != null &&
                        t.BaseType.IsGenericType &&
                        t.BaseType.GetGenericTypeDefinition() ==
                        typeof(InvariantRuleHandlerBase<>) &&
                        t.BaseType.GetGenericArguments()[0] ==
                        typeof(TAggregate))
                    .ToList();
        }
    }
}