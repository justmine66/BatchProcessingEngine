using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatchProcessingEngine
{
    /// <summary>
    /// 数据提供者
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// 根据上下文，获取批数据。
        /// </summary>
        /// <typeparam name="TPayLoad">数据类型</typeparam>
        /// <param name="context">上下文</param>
        /// <returns>批数据</returns>
        Task<IEnumerable<TPayLoad>> GetBatchAsync<TPayLoad>(ProcessingContext context);

        /// <summary>
        /// 获取检查点标识
        /// </summary>
        /// <returns></returns>
        Task<int> GetCheckPointIdAsync();

        /// <summary>
        /// 获取数据总量
        /// </summary>
        /// <returns></returns>
        Task<int> GetTotalSizeAsync();
    }
}
