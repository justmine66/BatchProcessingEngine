namespace BatchProcessingEngine.Configuration
{
    public class ProcessingOptions
    {
        /// <summary>
        /// 大批次尺寸
        /// </summary>
        /// <remarks>
        /// 假如：100跳数据，LargeBatchSize为50，则系统将分两个大批次并行处理。
        /// </remarks>
        public int LargeBatchSize { get; set; } = 1;

        /// <summary>
        /// 小批次尺寸
        /// </summary>
        /// <remarks>
        /// 假如：LargeBatchSize为50，MicroBatchSize为10，那么系统会将每个大批次分成5个微批次串行处理。
        /// </remarks>
        public int MicroBatchSize { get; set; } = 1;
    }
}
