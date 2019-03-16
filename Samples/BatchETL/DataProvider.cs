using BatchProcessingEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatchETL
{
    public class DataProvider : IDataProvider
    {
        public async Task<dynamic> GetBatchDataAsync(ProcessingContext context)
        {
            var smallBatch = context.MetaData.SmallBatch;
            var largeBatch = context.MetaData.LargeBatch;
            var batchSize = smallBatch.BatchSize;
            var offsetTo = largeBatch.CheckPointId + smallBatch.BatchSequence * batchSize;
            var offsetFrom = offsetTo - batchSize;

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

    internal static class InMemoryDb
    {
        internal static List<PayLoad> Payloads = new List<PayLoad>();

        static InMemoryDb()
        {
            for (var i = 1; i <= 200; i++)
                Payloads.Add(new PayLoad() { Id = i, Content = $"第{i}条内容" });
        }
    }
}
