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
                configure.SetBatchProcessingEngine(options.GetValue<int>(nameof(ProcessingOptions.LargeBatchSize)), options.GetValue<int>(nameof(ProcessingOptions.MicroBatchSize)));
            });

            container.AddSingleton<IDataProvider, MicroBatchDataProvider>();
            container.TryAddEnumerable(ServiceDescriptor.Singleton<IApplicationListener, ProcessingEngineCompletedNotifier>());
        }
    }
}
