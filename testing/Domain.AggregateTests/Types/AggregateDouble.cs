using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Aggregates;

namespace Domain.AggregateTests.Types
{
    public class AggregateDouble
    : AggregateRootBase
    {
        public Guid Id { get; }

        private readonly Assembly _assemblyContainingHandlers;

        public AggregateDouble(Guid id, Assembly assemblyContainingHandlers)
        {
            Id = id;
            _assemblyContainingHandlers = assemblyContainingHandlers;
        }
        protected override Guid GetId()
        {
            return Id;
        }

        protected override Assembly GetAssemblyContainingHandlers()
        {
            return _assemblyContainingHandlers;
        }

        public string Name { get; set; } = "";
    }
}
