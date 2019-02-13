using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BatchProcessingEngine
{
    public class ProcessingEngineBuilder : IEngineBuilder
    {
        private readonly IServiceProvider _container;
        private readonly List<Action<IProcessingPipeLineBuilder>> _configures = new List<Action<IProcessingPipeLineBuilder>>();
        public ProcessingEngineBuilder(IServiceProvider container)
        {
            _container = container;
        }

        public IEngineBuilder Configure(Action<IProcessingPipeLineBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        public IEngine Build()
        {
            var pipeLineBuilder = new ProcessingPipeLineBuilder();
            foreach (var configure in _configures)
            {
                configure(pipeLineBuilder);
            }

            var contextBuilder = new ProcessingContextBuilder()
                .AddDataHandler(pipeLineBuilder.Build());

            var engine = _container.GetRequiredService<IEngine>();
            return engine;
        }
    }
}
