using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.UnitTests.Types
{
    public class AggregateA : AggregateRootBase
    {
        protected override void When(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }

        protected override Guid GetId()
        {
            throw new NotImplementedException();
        }

 
    }
}
