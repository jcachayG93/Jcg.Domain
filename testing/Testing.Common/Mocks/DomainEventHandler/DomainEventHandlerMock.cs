using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;
using Domain.Core.UnitTests.TestCommon;
using Moq;

namespace Testing.Common.Mocks.DomainEventHandler
{
    public class DomainEventHandlerMock<TAggregate>
    : DomainEventHandlerBase<TAggregate>
        where TAggregate : AggregateRootBase
    {
        public DomainEventHandlerHandleArgs<TAggregate>? HandleArgs { get; private set; } = null;


        public bool PerformHandlingReturns { get; set; } 
        internal override void Handle(TAggregate aggregate, IDomainEvent domainEvent)
        {
            HandleArgs = new(aggregate, domainEvent);
        }

        protected override bool PerformHandling(TAggregate aggregate, IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
