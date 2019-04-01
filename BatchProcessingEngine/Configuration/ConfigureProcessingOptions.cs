using Microsoft.Extensions.Options;

namespace BatchProcessingEngine.Configuration
{
    public class ConfigureProcessingOptions : ConfigureOptions<ProcessingOptions>
    {
        public ConfigureProcessingOptions(float batchProcessingFactor = 1, float microBatchProcessingFactor = 0.2f) : base(options =>
                {
                    options.BatchProcessingFactor = batchProcessingFactor;
                    options.MicroBatchProcessingFactor = microBatchProcessingFactor;
                })
        { }
    }
}
