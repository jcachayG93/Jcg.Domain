using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Aggregates.DomainEvents
{
    /// <summary>
    /// A domain event, describing how to update the state of an aggregate
    /// </summary>
    public interface IDomainEvent
    {
        Guid AggregateId { get; }
    }
}
