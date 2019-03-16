using BatchProcessingEngine;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchETL
{
    public class DataProcessingPipeLineMiddleware
    {
        private readonly ILogger _logger;
        private readonly ProcessingDelegate _next;

        public DataProcessingPipeLineMiddleware(ILogger<DataProcessingPipeLineMiddleware> logger, ProcessingDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public Task InvokeAsync(ProcessingContext context)
        {
            if (!(context.Payloads is IEnumerable<PayLoad> payloads))
                return Task.CompletedTask;

            foreach (var payload in payloads)
                Console.WriteLine($@"[{context.MetaData.LargeBatch.BatchSequence}-{context.MetaData.SmallBatch.BatchSequence}]Start handling: {payload}");

            return Task.CompletedTask;
        }
    }
}
