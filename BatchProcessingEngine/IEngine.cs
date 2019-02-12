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
        /// 启动整个处理流程.
        /// </summary>
        Task StartAsync();
    }
}
