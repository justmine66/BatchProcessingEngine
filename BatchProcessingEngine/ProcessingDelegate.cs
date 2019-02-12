using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    public delegate Task ProcessingDelegate(ProcessingContext context);
}
