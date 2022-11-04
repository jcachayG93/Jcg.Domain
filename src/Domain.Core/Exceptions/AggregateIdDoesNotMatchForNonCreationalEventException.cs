using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Exceptions
{
    public class AggregateIdDoesNotMatchForNonCreationalEventException
    : DomainCoreException
    {
        public AggregateIdDoesNotMatchForNonCreationalEventException(
            Guid aggregateId, Guid eventId, string domainEventType)
        : base(
$"The Id of the Aggregate and the Domain Event did not match for a Non creational domain event type." +
$"The Aggregate.Id value was {aggregateId} and the domain event Id value was: {eventId}. The event type was" +
$"{domainEventType}. This exception is a quality check that ensures a domain event was not applied to the wrong aggregate instance by error."
            )
        {
            
        }
    }
}
