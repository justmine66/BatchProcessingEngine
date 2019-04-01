using BatchProcessingEngine;
using BatchProcessingEngine.Eventing;
using BatchProcessingEngine.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BatchETL
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection container)
        {
            container.AddProcessingServices();

            container.AddSingleton<IDataProvider, MicroBatchDataProvider>();
            container.TryAddEnumerable(ServiceDescriptor.Singleton<IApplicationListener, ProcessingEngineCompletedNotifier>());
        }
    }
}
