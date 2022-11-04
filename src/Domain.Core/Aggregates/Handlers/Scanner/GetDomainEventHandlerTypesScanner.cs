using System.Reflection;

namespace Domain.Core.Aggregates.Handlers.Scanner
{
    internal class GetDomainEventHandlerTypesScanner
    {
        public IEnumerable<Type> GetDomainEventHandlerTypes(Type aggregateType,
            Assembly assemblyToScan)
        {
            return assemblyToScan
                .GetTypes()
                .Where(t =>
                    t.IsClass &&
                    t.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() ==
                        typeof(IDomainEventHandler<>) &&
                        i.GetGenericArguments()[0] == aggregateType))
                .ToList();
        }
    }
}