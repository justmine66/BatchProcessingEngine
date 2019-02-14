using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    /// <summary>
    /// Defines middleware that can be added to the engine's processing pipeline.
    /// </summary>
    public interface IMiddleware
    {
        /// <summary>
        /// Data payloads handling method.
        /// </summary>
        /// <param name="context"><see cref="ProcessingContext"/> for the current processing pipeline.</param>
        /// <param name="next">The delegate representing the remaining middleware in the processing pipeline.</param>
        /// <returns></returns>
        Task InvokeAsync(ProcessingContext context, ProcessingDelegate next);
    }
}
