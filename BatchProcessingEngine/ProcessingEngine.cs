using BatchProcessingEngine.Exceptions;
using BatchProcessingEngine.WorkPool;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class ProcessingEngine : IEngine
    {
        private volatile int _started;
        private readonly IExecutor _executor;
        private readonly IEnumerable<IProcessorBuilder> _processor;

        public ProcessingEngine(IExecutor executor, IEnumerable<IProcessorBuilder> processor)
        {
            _executor = executor;
            _processor = processor;
        }

        public async Task StartAsync()
        {
            if (_processor == null ||
                !_processor.Any()) return;

            CheckOnlyStartedOnce();

            var rounds = _processor.Count();
            var workers = new Task[rounds];

            var index = 0;
            foreach (var processor in _processor)
            {
                workers[index++] = _executor.ExecuteAsync(processor.Build());
            }

            await Task.WhenAll(workers);
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
