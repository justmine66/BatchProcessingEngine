namespace BatchProcessingEngine.Configuration
{
    public class ProcessingOptions
    {
        /// <summary>
        /// 批次处理因子：并行批次数/数据总量，范围：(0 - 1]。
        /// </summary>
        /// <remarks>
        /// 分多少个大批次并行处理所有数据。
        /// 0.5：分2个大批次并行处理数据。
        /// 0.2：分5个大批次并行处理数据。
        /// 以此类推...
        /// 不能整除的情况下，将会多增加一个大批次。
        /// 此因子决定并行处理工人数量。
        /// </remarks>
        public float BatchProcessingFactor { get; set; } = 1;

        /// <summary>
        /// 微批处理因子：串行微批数/大批次数据总量，范围：(0 - 1]。
        /// </summary>
        /// <remarks>
        /// 分多少个阶段（微批）处理一个大批次。
        /// 0.5：分2个阶段串行处理一个大批次。
        /// 0.2：分5个阶段串行处理一个大批次。
        /// 以此类推...
        /// 不能整除的情况下，将会多增加一个微批。
        /// 此因子决定串行处理阶段数量。
        /// </remarks>
        public float MicroBatchProcessingFactor { get; set; } = 1;
    }
}
