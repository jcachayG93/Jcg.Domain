using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Exceptions
{
    public abstract class ValueObjectException : Exception
    {
        protected ValueObjectException(string error) : base(error)
        {
            
        }

        protected ValueObjectException()
        {
            
        }
    }
}
