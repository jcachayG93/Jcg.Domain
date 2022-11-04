using Domain.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.ValueObjects
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
