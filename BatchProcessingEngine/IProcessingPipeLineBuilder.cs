using System;

namespace BatchProcessingEngine
{
    public interface IProcessingPipeLineBuilder
    {
        IServiceProvider Services { get; set; }

        /// <summary>
        /// Adds a middleware delegate to the engine's process pipeline.
        /// </summary>
        /// <param name="middleware">The middleware delegate.</param>
        /// <returns>The <see cref="ProcessingPipeLineBuilder"/>.</returns>
        IProcessingPipeLineBuilder UseMiddleware(Func<ProcessingDelegate, ProcessingDelegate> middleware);

        /// <summary>
        /// Builds the delegate used by this engine to process data.
        /// </summary>
        /// <returns></returns>
        ProcessingDelegate Build();
    }
}
