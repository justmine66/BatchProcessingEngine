using BatchProcessingEngine.Exceptions;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class ProcessingEngine : IEngine
    {
        private volatile int _started;

        private readonly ILogger _logger;
        private readonly IScheduler _scheduler;
        private readonly IDataProvider _dataProvider;
        private readonly ProcessingContextBuilder _contextBuilder;

        public ProcessingEngine(
            IScheduler scheduler,
            IDataProvider dataProvider,
            ILogger<ProcessingEngine> logger,
            ProcessingContextBuilder contextBuilder)
        {
            _dataProvider = dataProvider;
            _scheduler = scheduler;
            _logger = logger;
            _contextBuilder = contextBuilder;
        }

        public async Task StartAsync()
        {
            CheckOnlyStartedOnce();

            var totalSize = await _dataProvider.GetTotalSizeAsync();
            if (totalSize <= 0)
            {
                _logger.LogCritical($"[{AppMode.Environment}]Engine Startup failure: Total size to processed is {totalSize}.");
                return;
            }
            var checkPoint = await _dataProvider.GetCheckPointIdAsync();

            var context = _contextBuilder
                .AddTotalSize(totalSize)
                .AddCheckPoint(checkPoint)
                .Build();

            await _scheduler.ScheduleAsync(context);
        }

        private void CheckOnlyStartedOnce()
        {
            if (Interlocked.Exchange(ref _started, 1) == 1)
            {
                throw new IllegalStateException($"{nameof(CheckOnlyStartedOnce)} must only be called once.");
            }
        }
    }
}
