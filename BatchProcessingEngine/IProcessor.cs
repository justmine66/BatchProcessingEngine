using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public interface IProcessor
    {
        Task RunAsync(ProcessingContext context);
    }
}
