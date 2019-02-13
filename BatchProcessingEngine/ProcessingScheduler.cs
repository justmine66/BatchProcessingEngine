using BatchProcessingEngine.Extensions;
using BatchProcessingEngine.WorkPool;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class ProcessingScheduler : IScheduler
    {
        private readonly ILogger _logger;
        private readonly IExecutor _executor;
        private readonly IServiceProvider _serviceProvider;

        public ProcessingScheduler(
            ILogger<ProcessingScheduler> logger,
            IServiceProvider serviceProvider,
            IExecutor executor)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _executor = executor;
        }

        public async Task ScheduleAsync(ProcessingContext context)
        {
            var rounds = context.GetParallelRounds();
            var workers = new Task[rounds];

            _logger.LogInformation($"[{AppMode.Environment}]Data Processing Engine is scheduling");

            for (var j = 0; j < rounds; j++)
            {
                context.LargeBatch.BatchNumber = j + 1;

                using (var scope = _serviceProvider.CreateScope())
                {
                    var processor = scope.ServiceProvider.GetRequiredService<IProcessor>();
                    workers[j] = _executor.ExecuteAsync(processor, context.Copy());
                }
            }

            await Task.WhenAll(workers).ConfigureAwait(false);

            _logger.LogInformation($"The work has been done.");
        }
    }
}
