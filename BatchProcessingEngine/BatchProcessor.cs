using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public class BatchProcessor : IProcessor
    {
        private volatile int _running;

        private static class RunningState
        {
            internal const int Idle = 0;
            internal const int Running = Idle + 1;
        }

        private readonly ILogger _logger;
        private readonly IDataProvider _dataProvider;
        public BatchProcessor(ILogger<BatchProcessor> logger, IDataProvider dataProvider)
        {
            _logger = logger;
            _dataProvider = dataProvider;
        }

        public async Task RunAsync(ProcessingContext context)
        {
            if (Interlocked.CompareExchange(ref _running, RunningState.Running, RunningState.Idle) == RunningState.Idle)
            {
                // 分小批次平滑处理大批次。
                var largeBatchSize = context.LargeBatch.BatchSize;
                var smallBatchSize = context.SmallBatch.BatchSize;
                var round = context.SmallBatch.BatchSequence <= 0 ? 1 : context.SmallBatch.BatchSequence;
                var batchSize = largeBatchSize > smallBatchSize ? smallBatchSize : largeBatchSize;

                var start = 0;
                using (_logger.BeginScope($"Data Batch Processor {context}"))
                {
                    try
                    {
                        while (start <= largeBatchSize)
                        {
                            // double check
                            if (_running != RunningState.Running)
                                continue;

                            _logger.LogInformation(
                                $"{Environment.NewLine}Small batch: the {round} round[{start}-{start + batchSize}] beginning...");

                            var payloads = await _dataProvider.GetBatchDataAsync(context);
                            await context.DataHandler(context, payloads);

                            _logger.LogInformation($"Small batch: the {round} round[{start}-{start + batchSize}] ended.");

                            start += batchSize;
                            context.SmallBatch.BatchSequence = round++;
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"{nameof(RunAsync)} has a unknown exception: {e}.");
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _running, RunningState.Idle);
                    }
                }
            }
            else
            {
                _logger.LogError("Thread is already running.");
            }
        }
    }
}
