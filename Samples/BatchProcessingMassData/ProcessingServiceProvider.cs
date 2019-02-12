using BatchProcessingEngine.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BatchProcessingMassData
{
    public class ProcessingServiceProvider
    {
        public static IServiceProvider GetServiceProvider()
        {
            var container = new ServiceCollection()
                .AddProcessingServices()
                .BuildServiceProvider();

            return container;
        }
    }
}
