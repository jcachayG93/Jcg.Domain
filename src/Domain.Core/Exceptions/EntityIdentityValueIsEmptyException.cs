namespace Domain.Core.Exceptions;

public class EntityIdentityValueIsEmptyException : ValueObjectException
{
    public EntityIdentityValueIsEmptyException()
        : base("Entity Identity Value can't be empty")
    {
    }
}