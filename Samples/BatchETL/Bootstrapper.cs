using BatchProcessingEngine;
using BatchProcessingEngine.Configuration;
using BatchProcessingEngine.Extensions;
using BatchProcessingEngine.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BatchETL
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection container, IConfiguration configuration)
        {
            container.AddProcessingServices(configure =>
            {
                var options = configuration.GetSection(nameof(ProcessingOptions));
                configure.SetBatchProcessingEngine(options.GetValue<float>(nameof(ProcessingOptions.BatchProcessingFactor)), options.GetValue<float>(nameof(ProcessingOptions.MicroBatchProcessingFactor)));
            });

            container.AddSingleton<IDataProvider, MicroBatchDataProvider>();
            container.TryAddEnumerable(ServiceDescriptor.Singleton<IApplicationListener, ProcessingEngineCompletedNotifier>());
        }
    }
}
