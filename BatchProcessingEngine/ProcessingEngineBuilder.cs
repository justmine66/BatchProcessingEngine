using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

        public IEngineBuilder ConfigureProcessingPipeLine(Action<IProcessingPipeLineBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        public IEngine Build()
        {
            var pipeLineBuilder = new ProcessingPipeLineBuilder { Services = _container };
            foreach (var configure in _configures)
            {
                configure(pipeLineBuilder);
            }

            var dataProvider = _container.GetRequiredService<IDataProvider>();
            var options = _container.GetRequiredService<IOptions<ProcessingOptions>>();
            var scheduler = _container.GetRequiredService<IScheduler>();
            var logger = _container.GetRequiredService<ILogger<ProcessingEngine>>();
            var contextBuilder = new ProcessingContextBuilder()
                .AddDataHandler(pipeLineBuilder.Build())
                .AddOptions(options.Value);

            var engine = new ProcessingEngine(scheduler, dataProvider, logger, contextBuilder);
            return engine;
        }
    }
}
