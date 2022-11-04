using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates.DomainEventHandlers;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.AggregateTests.Types
{
    public record EventWithNoHandlers(Guid AggregateId) : ICreationalDomainEvent;
}
