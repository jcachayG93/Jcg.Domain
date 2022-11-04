using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates;

namespace Domain.Core.UnitTests.Types
{
    public class AggregateA : AggregateRootBase
    {
        protected override Guid GetId()
        {
            throw new NotImplementedException();
        }

        protected override Assembly GetAssemblyContainingHandlers()
        {
            throw new NotImplementedException();
        }
    }
}
