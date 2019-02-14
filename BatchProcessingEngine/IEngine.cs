using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    /// <summary>
    /// 批处理引擎
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 启动整个数据处理管道.
        /// </summary>
        Task StartAsync();
    }
}
