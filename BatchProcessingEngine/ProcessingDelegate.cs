using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    /// <summary>
    /// A function that can process payloads.
    /// </summary>
    /// <param name="context">The <see cref="ProcessingContext"/> for the payloads.</param>
    /// <returns>A task that represents the completion of payloads processing.</returns>
    public delegate Task ProcessingDelegate(ProcessingContext context);
}
