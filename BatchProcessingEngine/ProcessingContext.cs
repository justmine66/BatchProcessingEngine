﻿namespace BatchProcessingEngine
{
    public class ProcessingContext
    {
        public ProcessingDelegate DataHandler { get; set; }

        /// <summary>
        /// 数据总吞吐量
        /// </summary>
        public int TotalThroughput { get; set; }

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
        /// 检查点标识（上一次处理截止点）
        /// </summary>
        public int CheckPointId { get; set; }
    }
}