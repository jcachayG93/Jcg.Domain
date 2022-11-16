using Jcg.Domain.Exceptions;

namespace Jcg.Domain.ValueObjects
{
    /// <summary>
    /// An Entity identity
    /// </summary>
    public abstract record EntityIdentityBase
    {
        protected EntityIdentityBase(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EntityIdentityValueIsEmptyException();
            }

            Id = id;
        }

        public Guid Id { get; }
    }
}
