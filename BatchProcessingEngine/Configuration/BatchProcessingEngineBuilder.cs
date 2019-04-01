using Microsoft.Extensions.DependencyInjection;

namespace BatchProcessingEngine.Configuration
{
    public class BatchProcessingEngineBuilder : IBatchProcessingEngineBuilder
    {
        public BatchProcessingEngineBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
