using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates;
using Domain.Core.Aggregates.DomainEvents;

namespace Domain.AggregateTests.Types
{
    public class AggregateDouble
    : AggregateRootBase
    {
        public IDomainEvent? Test_WhenArgs { get; private set; } = null;
        public Guid Id { get; }

        private readonly Assembly _assemblyContainingHandlers;

        public AggregateDouble(Guid id, Assembly assemblyContainingHandlers)
        {
            Id = id;
            _assemblyContainingHandlers = assemblyContainingHandlers;
        }

        protected override void When(IDomainEvent domainEvent)
        {
            Test_WhenArgs = domainEvent;
        }

        protected override Guid GetId()
        {
            return Id;
        }



        public string Name { get; set; } = "";
    }
}
