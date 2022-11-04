namespace Domain.Core.Exceptions;

public class EntityIdentityValueIsEmptyException : DomainCoreException
{
    public EntityIdentityValueIsEmptyException()
        : base("Entity Identity Value can't be empty")
    {
    }
}