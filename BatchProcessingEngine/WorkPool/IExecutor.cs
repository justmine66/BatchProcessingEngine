using System.Threading.Tasks;

namespace BatchProcessingEngine.WorkPool
{
    public interface IExecutor
    {
        /// <summary>
        /// Execute the given commands asynchronously in other thread.
        /// </summary>
        /// <param name="command">Needs to be executed.</param>
        Task ExecuteAsync(IProcessor command);
    }
}
