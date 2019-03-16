using System;

namespace BatchProcessingEngine
{
    public class ProcessingContext
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        public IServiceProvider Services { get; set; }

        /// <summary>
        /// 数据载荷
        /// </summary>
        public dynamic Payloads { get; set; }

        /// <summary>
        /// 数据载荷处理者
        /// </summary>
        public ProcessingDelegate DataHandler { get; set; }

        /// <summary>
        /// 处理元数据
        /// </summary>
        public ProcessingMetaData MetaData { get; set; }
    }

    public class ProcessingMetaData
    {
        /// <summary>
        /// 数据总吞吐量
        /// </summary>
        public int TotalThroughput { get; set; }

        /// <summary>
        /// 全局偏移量
        /// </summary>
        public int GlobalOffSet { get; set; }

        /// <summary>
        /// 大批次
        /// </summary>
        public BatchDescriptor LargeBatch { get; set; }

        /// <summary>
        /// 小批次
        /// </summary>
        public BatchDescriptor SmallBatch { get; set; }
    }

    public class BatchDescriptor
    {
        /// <summary>
        /// 批次序列，默认（第一轮）：1。
        /// </summary>
        public int BatchSequence { get; set; } = 1;

        /// <summary>
        /// 批次数量
        /// </summary>
        public int BatchSize { get; set; } = 100;

        /// <summary>
        /// 批次偏移量，范围：小于等于 <see cref="BatchSize"/>。
        /// </summary>
        public int BatchOffset { get; set; }

        /// <summary>
        /// 检查点标识（上一次处理截止点）
        /// </summary>
        public int CheckPointId { get; set; }

        public override string ToString()
        {
            return $"{{BatchSequence: {BatchSequence},BatchSize: {BatchSize},BatchOffset: {BatchOffset},CheckPointId: {CheckPointId}}}";
        }

        /// <summary>
        /// 深复制
        /// </summary>
        /// <returns></returns>
        public BatchDescriptor Copy()
        {
            return new BatchDescriptor()
            {
                BatchSequence = this.BatchSequence,
                BatchSize = this.BatchSize,
                BatchOffset = this.BatchOffset,
                CheckPointId = this.CheckPointId
            };
        }

        /// <summary>
        /// 浅克隆
        /// </summary>
        /// <returns></returns>
        public BatchDescriptor Clone()
        {
            return (BatchDescriptor)this.MemberwiseClone();
        }
    }
}
