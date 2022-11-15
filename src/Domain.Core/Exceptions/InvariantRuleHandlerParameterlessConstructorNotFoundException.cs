namespace Jcg.Domain.Exceptions
{
    public class InvariantRuleHandlerParameterlessConstructorNotFoundException
        : DomainCoreException
    {
        public InvariantRuleHandlerParameterlessConstructorNotFoundException(
            string handlerTypeName)
            : base(
                $"Parameter-less constructor not found for handler: {handlerTypeName}")
        {
        }
    }
}