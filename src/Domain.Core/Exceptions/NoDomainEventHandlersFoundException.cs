namespace Domain.Core.Exceptions
{
    public class NoDomainEventHandlersFoundException
        : DomainCoreException
    {
        public NoDomainEventHandlersFoundException(
            string aggregateTypeName)
            : base(
                $"No handlers for aggregate of type {aggregateTypeName} where found in the assembly")
        {
        }
    }
}