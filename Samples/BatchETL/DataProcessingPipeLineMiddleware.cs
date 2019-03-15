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

        public DataProcessingPipeLineMiddleware(ILogger logger)
        {
            _logger = logger;
        }

        public Task InvokeAsync(ProcessingContext context)
        {
            if (!(context.Payloads is IEnumerable<int> payloads))
                return Task.CompletedTask;

            foreach (var payload in payloads)
                Console.WriteLine($@"Start handling: {payload}");

            return Task.CompletedTask;
        }
    }
}
