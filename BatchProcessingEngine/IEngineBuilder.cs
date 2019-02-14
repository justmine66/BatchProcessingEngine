using System;

namespace BatchProcessingEngine
{
    public interface IEngineBuilder
    {
        IEngineBuilder ConfigureProcessingPipeLine(Action<IProcessingPipeLineBuilder> configure);
        IEngine Build();
    }
}
