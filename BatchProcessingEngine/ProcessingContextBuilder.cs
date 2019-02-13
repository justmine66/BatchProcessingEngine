namespace BatchProcessingEngine
{
    public class ProcessingContextBuilder
    {
        private ProcessingOptions _options;
        private ProcessingDelegate _dataHandler;
        private int _totalThroughput;

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

            return new ProcessingContext
            {
                DataHandler = _dataHandler,
                TotalThroughput = _totalThroughput,
                LargeBatch = largeBatch
            };
        }
    }
}
