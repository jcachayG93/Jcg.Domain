namespace Jcg.Domain.Exceptions
{
    public abstract class DomainCoreException : Exception
    {
        protected DomainCoreException(string error) : base(error)
        {
        }

        protected DomainCoreException()
        {
        }
    }
}