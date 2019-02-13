using System;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class BatchProcessor : IProcessor
    {
        public Task RunAsync(ProcessingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
