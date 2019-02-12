using BatchProcessingEngine.WorkPool;
using System;
using System.Collections.Generic;

namespace BatchProcessingEngine
{
    public class ProcessingEngineBuilder : IEngineBuilder
    {
        private List<Action<IProcessorBuilder>> _configures = new List<Action<IProcessorBuilder>>();
        private IExecutor _executor;

        public IEngineBuilder UseExecutor(IExecutor executor)
        {
            _executor = executor;
            return this;
        }

        public IEngineBuilder Configure(Action<IProcessorBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        public IEngine Build()
        {
            return new ProcessingEngine(_executor, null);
        }
    }
}
