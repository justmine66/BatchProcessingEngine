using System;

namespace BatchProcessingEngine
{
    public interface IProcessingPipeLineBuilder
    {
        IProcessingPipeLineBuilder Use(Func<ProcessingDelegate, ProcessingDelegate> middleware);
        ProcessingDelegate Build();
    }
}
