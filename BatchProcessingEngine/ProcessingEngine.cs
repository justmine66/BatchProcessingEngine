using BatchProcessingEngine.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class ProcessingEngine : IEngine
    {
        private volatile int _started;

        private readonly IDataProvider _dataProvider;
        private readonly ProcessingOptions _options;
        private readonly IScheduler _scheduler;
        private readonly ILogger _logger;
        private readonly ProcessingDelegate _dataHandler;

        public ProcessingEngine(
            IDataProvider dataProvider,
            IOptions<ProcessingOptions> options,
            IScheduler scheduler,
            ILogger<ProcessingEngine> logger)
        {
            _dataProvider = dataProvider;
            _scheduler = scheduler;
            _logger = logger;
            _options = options.Value;
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

            var context = new ProcessingContextBuilder()
                .AddTotalSize(totalSize)
                .AddOptions(_options)
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
