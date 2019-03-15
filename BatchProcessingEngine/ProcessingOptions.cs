namespace BatchProcessingEngine
{
    public class ProcessingOptions
    {
        /// <summary>
        /// 处理因子：并行批次数/数据总量，范围：(0 - 1]
        /// </summary>
        /// <remarks>
        /// 1：分一个大批次串行处理所有数据。
        /// 0.5：分2个大批次并行处理数据。
        /// 0.2：分5个大批次并行处理数据。
        /// 以此类推...
        /// 不能整除的情况下，将会多增加一个大批次。
        /// </remarks>
        public float ProcessingFactor { get; set; } = 1;
    }
}
