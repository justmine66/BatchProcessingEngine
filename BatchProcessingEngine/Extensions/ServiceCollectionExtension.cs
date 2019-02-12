using BatchProcessingEngine.WorkPool;
using Microsoft.Extensions.DependencyInjection;

namespace BatchProcessingEngine.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProcessingServices(this IServiceCollection container)
        {
            container.AddScoped<IExecutor, BasicExecutor>();
            container.AddScoped<IEngine, ProcessingEngine>();
            container.AddScoped<IEngineBuilder, ProcessingEngineBuilder>();

            return container;
        }
    }
}
