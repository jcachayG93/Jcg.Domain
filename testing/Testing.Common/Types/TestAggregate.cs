using System.Reflection;
using Domain.Core.Aggregates;

namespace Domain.Core.UnitTests.TestCommon
{
    public class TestAggregate
        : AggregateRootBase
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