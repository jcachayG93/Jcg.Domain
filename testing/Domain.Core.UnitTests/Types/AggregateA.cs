using System.Reflection;
using Domain.Core.Aggregates;

namespace Domain.Core.UnitTests.Types
{
    public class AggregateA : AggregateRootBase
    {
        /// <inheritdoc />
        protected override Guid GetId()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override Assembly GetAssemblyContainingHandlers()
        {
            throw new NotImplementedException();
        }
    }
}