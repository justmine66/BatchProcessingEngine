using System;

namespace BatchProcessingEngine
{
    public class ProcessingContextBuilder
    {
        private ProcessingOptions _options;
        private ProcessingDelegate _dataHandler;
        private int _totalThroughput;
        private IServiceProvider _container;

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
            var largeBatch = new BatchDescriptor { BatchSize = (int)(_options.ProcessingFactor * _totalThroughput) };
            var smallBatch = new BatchDescriptor();

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
