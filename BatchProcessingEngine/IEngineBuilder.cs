using System;

namespace BatchProcessingEngine
{
    public interface IEngineBuilder
    {
        IEngineBuilder Configure(Action<IProcessingPipeLineBuilder> configure);
        IEngine Build();
    }
}
