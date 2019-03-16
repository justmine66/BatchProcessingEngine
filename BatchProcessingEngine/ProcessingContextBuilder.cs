using System;

namespace BatchProcessingEngine
{
    public class ProcessingContextBuilder
    {
        private ProcessingOptions _options;
        private ProcessingDelegate _dataHandler;
        private int _totalThroughput;
        private IServiceProvider _container;
        private int _checkPoint;

        public ProcessingContextBuilder AddServices(IServiceProvider services)
        {
            _container = services;

            return this;
        }

        public ProcessingContextBuilder AddTotalSize(int totalThroughput)
        {
            _totalThroughput = totalThroughput;

            return this;
        }

        public ProcessingContextBuilder AddCheckPoint(int checkPoint)
        {
            _checkPoint = checkPoint;

            return this;
        }

        public ProcessingContextBuilder AddOptions(ProcessingOptions options)
        {
            _options = options;
            return this;
        }

        public ProcessingContextBuilder AddDataHandler(ProcessingDelegate dataHandler)
        {
            _dataHandler = dataHandler;
            return this;
        }

        public ProcessingContext Build()
        {
            Assert.Range(_options.BatchProcessingFactor, 0, 1);
            Assert.Range(_options.MicroBatchProcessingFactor, 0, 1);

            var largeBatch = new BatchDescriptor
            {
                BatchSize = (int)(_options.BatchProcessingFactor * _totalThroughput),
                CheckPointId = _checkPoint
            };
            var smallBatch = new BatchDescriptor() { BatchSize = (int)(_options.MicroBatchProcessingFactor * largeBatch.BatchSize) };

            return new ProcessingContext
            {
                Services = _container,
                DataHandler = _dataHandler,
                MetaData = new ProcessingMetaData()
                {
                    TotalThroughput = _totalThroughput,
                    LargeBatch = largeBatch,
                    SmallBatch = smallBatch
                }
            };
        }
    }
}
