using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Aggregates.Factories
{
    
    internal class DomainEventHandlerPipelineFactory
    {
        private static DomainEventHandlerPipelineFactory? _instance = null;

        private DomainEventHandlerPipelineFactory(Assembly assemblyToScan)
        {
            
        }

        public static DomainEventHandlerPipelineFactory GetInstance(Assembly assemblyToScan)
        {
            var lockObject = new object();

            lock (lockObject)
            {
                if (_instance is null)
                {
                    _instance =
                        new DomainEventHandlerPipelineFactory(assemblyToScan);
                }

                return _instance;
            }
        }
    }
}
