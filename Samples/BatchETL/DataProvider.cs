using BatchProcessingEngine;
using System;
using System.Threading.Tasks;

namespace BatchETL
{
    public class DataProvider : IDataProvider
    {
        public Task<dynamic> GetBatchDataAsync(ProcessingContext context)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCheckPointIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalSizeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
