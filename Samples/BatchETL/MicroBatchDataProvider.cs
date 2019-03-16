using BatchProcessingEngine;
using BatchProcessingEngine.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace BatchETL
{
    public class MicroBatchDataProvider : IDataProvider
    {
        public async Task<dynamic> GetBatchDataAsync(ProcessingContext context)
        {
            var batchSize = context.MetaData.SmallBatch.BatchSize;
            var offsetFrom = context.BatchOffsetFrom();
            var offsetTo = offsetFrom + batchSize;

            var payloads = InMemoryDb.Payloads
                .Where(it => it.Id >= offsetFrom && it.Id < offsetTo)
                .Take(batchSize);

            return payloads;
        }

        public Task<int> GetCheckPointIdAsync()
        {
            return Task.FromResult(InMemoryDb.Payloads.First().Id);
        }

        public Task<int> GetTotalSizeAsync()
        {
            return Task.FromResult(InMemoryDb.Payloads.Count);
        }
    }
}
