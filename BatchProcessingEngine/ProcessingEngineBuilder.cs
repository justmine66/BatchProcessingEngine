using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using BatchProcessingEngine.Eventing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BatchProcessingEngine
{
    public class ProcessingEngineBuilder : IEngineBuilder
    {
        private readonly IServiceProvider _serviceLocator;
        private readonly List<Action<IProcessingPipeLineBuilder>> _configures = new List<Action<IProcessingPipeLineBuilder>>();
        public ProcessingEngineBuilder(IServiceProvider serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IEngineBuilder ConfigureProcessingPipeLine(Action<IProcessingPipeLineBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        public IEngine Build()
        {
            var pipeLineBuilder = new ProcessingPipeLineBuilder { Services = _serviceLocator };
            foreach (var configure in _configures)
            {
                configure(pipeLineBuilder);
            }

            var dataProvider = _serviceLocator.GetRequiredService<IDataProvider>();
            var options = _serviceLocator.GetRequiredService<IOptions<ProcessingOptions>>();
            var scheduler = _serviceLocator.GetRequiredService<IScheduler>();
            var logger = _serviceLocator.GetRequiredService<ILogger<ProcessingEngine>>();
            var source = _serviceLocator.GetRequiredService<IApplicationListenerSource>();
            var contextBuilder = new ProcessingContextBuilder()
                .AddServices(_serviceLocator)
                .AddDataHandler(pipeLineBuilder.Build())
                .AddOptions(options.Value);

            var engine = new ProcessingEngine(scheduler, source, dataProvider, logger, contextBuilder);
            return engine;
        }
    }
}
