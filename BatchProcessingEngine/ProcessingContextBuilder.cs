namespace BatchProcessingEngine
{
    public class ProcessingContextBuilder
    {
        private ProcessingOptions _options;
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

        public ProcessingContext Build()
        {
            return new ProcessingContext
            {
                TotalThroughput = _totalThroughput,
                LargeBatch = new LargeBatch() { BatchSize = (int)(_options.ProcessingFactor * _totalThroughput) }
            };
        }
    }
}
