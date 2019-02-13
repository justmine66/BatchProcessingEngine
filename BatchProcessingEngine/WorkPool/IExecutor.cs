using System.Threading.Tasks;

namespace BatchProcessingEngine.WorkPool
{
    public interface IExecutor
    {
        /// <summary>
        /// Execute the given commands asynchronously in other thread.
        /// </summary>
        /// <param name="command">Needs to be executed.</param>
        /// <param name="context">The processing context.</param>
        Task ExecuteAsync(IProcessor command, ProcessingContext context);
    }
}
