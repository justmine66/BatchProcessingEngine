using BatchProcessingEngine;
using BatchProcessingEngine.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace BatchETL
{
    public class MicroBatchDataProviderOne : IDataProvider
    {
        public async Task<dynamic> GetBatchDataAsync(ProcessingContext context)
        {
            var batchSize = context.MetaData.SmallBatch.BatchSize;
            var checkPointId = context.MetaData.LargeBatch.CheckPointId;

            var payloads = InMemoryDb.IrregularPayloads
                .Where(it => it.Id > checkPointId)
                .Take(batchSize);

            return payloads;
        }

        public Task<int> GetCheckPointIdAsync()
        {
            return Task.FromResult(InMemoryDb.IrregularPayloads.Min(it => it.Id));
        }

        public Task<int> GetTotalSizeAsync()
        {
            return Task.FromResult(InMemoryDb.IrregularPayloads.Count);
        }
    }
}
