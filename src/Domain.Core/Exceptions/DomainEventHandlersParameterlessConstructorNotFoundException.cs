using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Exceptions
{
    public class DomainEventHandlersParameterlessConstructorNotFoundException
    : DomainCoreException
    {
        public DomainEventHandlersParameterlessConstructorNotFoundException(
            string handlerTypeName):base($"Parameter-less constructor not found for handler: {handlerTypeName}")
        {
            
        }
    }
}
