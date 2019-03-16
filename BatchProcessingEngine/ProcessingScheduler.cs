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
        private readonly IServiceProvider _serviceProvider;

        public ProcessingScheduler(ILogger<ProcessingScheduler> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task ScheduleAsync(ProcessingContext context)
        {
            var rounds = context.GetParallelRounds();
            var workers = new Task[rounds];

            _logger.LogInformation($"[{AppMode.Environment}]Data Processing Engine is scheduling");

            for (var j = 0; j < rounds; j++)
            {
                var state = context.Copy();
                state.MetaData.LargeBatch.BatchSequence = j + 1;
                state.MetaData.GlobalOffSet = j * state.MetaData.LargeBatch.BatchSize;

                using (var scope = _serviceProvider.CreateScope())
                {
                    var processor = scope.ServiceProvider.GetRequiredService<IProcessor>();
                    var executor = scope.ServiceProvider.GetRequiredService<IExecutor>();
                    workers[j] = executor.ExecuteAsync(processor, state);
                }
            }

            await Task.WhenAll(workers).ContinueWith(task => _logger.LogInformation($"The work[ParallelRounds: {rounds}] has been done.")).ConfigureAwait(false);
        }
    }
}
