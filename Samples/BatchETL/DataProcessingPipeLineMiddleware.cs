using BatchProcessingEngine;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchETL
{
    public class DataProcessingPipeLineMiddleware
    {
        private readonly ILoadable _loader;
        private readonly ILogger _logger;

        public DataProcessingPipeLineMiddleware(ILoadable loader, ILogger logger)
        {
            _loader = loader;
            _logger = logger;
        }

        public async Task InvokeAsync(ProcessingContext context)
        {
            if (context.Payloads is IEnumerable<PayLoad> payloads)
            {
                _logger.LogInformation($"Loading");
                await _loader.LoadAsync(payloads);
                _logger.LogInformation($"Loaded");
            }
        }
    }
}
