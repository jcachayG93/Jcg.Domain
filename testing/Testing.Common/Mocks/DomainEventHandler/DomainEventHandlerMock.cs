using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;

namespace Testing.Common.Mocks.DomainEventHandler
{
    public class DomainEventHandlerMock<TAggregate>
        : DomainEventHandlerBase<TAggregate>
        where TAggregate : AggregateRootBase
    {
        public DomainEventHandlerHandleArgs<TAggregate>? HandleArgs
        {
            get;
            private set;
        } = null;


        public override void Handle(TAggregate aggregate,
            IDomainEvent domainEvent)
        {
            HandleArgs = new(aggregate, domainEvent);
        }

        protected override bool PerformHandling(TAggregate aggregate,
            IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}