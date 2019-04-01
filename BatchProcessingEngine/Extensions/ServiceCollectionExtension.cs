using BatchProcessingEngine.Configuration;
using BatchProcessingEngine.Eventing;
using BatchProcessingEngine.WorkPool;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace BatchProcessingEngine.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProcessingServices(this IServiceCollection container)
        {
            return AddProcessingServices(container, builder => { });
        }

        public static IServiceCollection AddProcessingServices(this IServiceCollection container, Action<IBatchProcessingEngineBuilder> configure)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            container.AddOptions();

            container.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<ProcessingOptions>>(
                new ConfigurationProcessingOptions()));

            container.AddSingleton<IEngine, ProcessingEngine>();
            container.AddSingleton<IScheduler, ProcessingScheduler>();

            container.AddScoped<IExecutor, BasicExecutor>();
            container.AddScoped<IProcessor, BatchProcessor>();

            container.AddSingleton<IMiddlewareFactory, MiddlewareFactory>();
            container.AddSingleton<IProcessingPipeLineBuilder, ProcessingPipeLineBuilder>();

            container.AddSingleton<IApplicationListenerSource, ApplicationListenerSourceImpl>();

            configure(new BatchProcessingEngineBuilder(container));

            return container;
        }
    }
}
