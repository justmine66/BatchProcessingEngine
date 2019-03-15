using BatchProcessingEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchETL
{
    public class DataProvider : IDataProvider
    {
        public async Task<dynamic> GetBatchDataAsync(ProcessingContext context)
        {
            var batchSize = context.MetaData.SmallBatch.BatchSize;
            var largeBatchLastId = context.MetaData.LargeBatch.CheckPointId + (context.MetaData.LargeBatch.BatchSequence - 1) * batchSize;

            return null;
        }

        public Task<int> GetCheckPointIdAsync()
        {
            return Task.FromResult(1);
        }

        public Task<int> GetTotalSizeAsync()
        {
            return Task.FromResult(100);
        }
    }

    internal static class InMemoryDb
    {
        internal static List<PayLoad> Payloads = new List<PayLoad>();

        static InMemoryDb()
        {
            for (var i = 1; i <= 1000; i++)
                Payloads.Add(new PayLoad() { Id = i, Content = $"第{i}条内容" });
        }
    }
}
