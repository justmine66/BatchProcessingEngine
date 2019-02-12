using System;

namespace BatchProcessingEngine
{
    public interface IEngineBuilder
    {
        /// <summary>
        /// 配置处理器
        /// </summary>
        IEngineBuilder Configure(Action<IProcessorBuilder> configure);
        IEngine Build();
    }
}
