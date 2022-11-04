using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.Core.UnitTests.Types
{
    public class DomainEventHandlerWithoutParameterlessConstructor
    : DomainEventHandlerBase<AggregateA>
    {
        public DomainEventHandlerWithoutParameterlessConstructor(
            string someValue)
        {
            
        }
        protected override bool PerformHandling(AggregateA aggregate, IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
