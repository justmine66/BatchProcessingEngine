using BatchProcessingEngine.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class ProcessingScheduler : IScheduler
    {
        private readonly ILogger _logger;

        public ProcessingScheduler(ILogger<ProcessingScheduler> logger)
        {
            _logger = logger;
        }

        public Task ScheduleAsync(ProcessingContext context)
        {
            var rounds = context.GetParallelRounds();

            throw new NotImplementedException();
        }
    }
}
