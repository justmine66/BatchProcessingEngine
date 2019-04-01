using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BatchProcessingEngine.Configuration
{
    public static class BatchProcessingEngineBuilderExtension
    {
        public static IBatchProcessingEngineBuilder SetBatchProcessingEngine(this IBatchProcessingEngineBuilder builder, int largeBatchSize = 1000, int microBatchSize = 100)
        {
            builder.Services.Add(ServiceDescriptor.Singleton<IConfigureOptions<ProcessingOptions>>(
                new ConfigureProcessingOptions(largeBatchSize, microBatchSize)));
            return builder;
        }
    }
}
