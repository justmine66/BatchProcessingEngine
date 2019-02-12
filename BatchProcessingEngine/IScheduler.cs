using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    /// <summary>
    /// 处理调度器
    /// </summary>
    public interface IScheduler
    {
        /// <summary>
        /// 编排处理器
        /// </summary>
        /// <returns></returns>
        Task ScheduleAsync(ProcessingContext context);
    }
}
