using System;
using BatchProcessingEngine.Configuration;

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
            var largeBatch = new BatchDescriptor
            {
                BatchSize = _options.LargeBatchSize,
                CheckPointId = _checkPoint
            };
            var smallBatch = new BatchDescriptor() { BatchSize = _options.MicroBatchSize };

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
