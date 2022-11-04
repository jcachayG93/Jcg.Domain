using System.Reflection;

namespace Domain.Core.Aggregates.Handlers.Scanner;

internal class GetAggregateTypesScanner
{
    /// <summary>
    ///     Returns all types implementing AggregateRootBase found in the specified assembly.
    /// </summary>
    /// <param name="assemblyToScan"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<Type> GetAggregateTypes(Assembly assemblyToScan)
    {
        return assemblyToScan.GetTypes()
            .Where(t =>
                t.IsClass &&
                t.BaseType != null &&
                t.BaseType == typeof(AggregateRootBase))
            .ToList();
    }
}