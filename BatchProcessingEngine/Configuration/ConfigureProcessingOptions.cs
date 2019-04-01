using Microsoft.Extensions.Options;

namespace BatchProcessingEngine.Configuration
{
    public class ConfigureProcessingOptions : ConfigureOptions<ProcessingOptions>
    {
        public ConfigureProcessingOptions(int largeBatchSize = 1000, int microBatchSize = 100) : base(options =>
                    {
                        options.LargeBatchSize = largeBatchSize;
                        options.MicroBatchSize = microBatchSize;
                    })
        { }
    }
}
