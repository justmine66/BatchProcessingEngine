namespace BatchProcessingEngine
{
    public class ProcessingContext
    {
        /// <summary>
        /// 数据总吞吐量
        /// </summary>
        public int TotalThroughput { get; set; }

        public LargeBatch LargeBatch { get; set; }
    }

    public class LargeBatch
    {
        /// <summary>
        /// 批次，默认（第一轮）：1。
        /// </summary>
        public int BatchNumber { get; set; } = 1;

        /// <summary>
        /// 批次数量
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// 检查点标识（上一次处理截止点）
        /// </summary>
        public int CheckPointId { get; set; }
    }
}
