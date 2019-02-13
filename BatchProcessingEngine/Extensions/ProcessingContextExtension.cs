namespace BatchProcessingEngine.Extensions
{
    public static class ProcessingContextExtension
    {
        /// <summary>
        /// 获取并行处理轮数
        /// </summary>
        /// <param name="context">处理上下文</param>
        public static int GetParallelRounds(this ProcessingContext context)
        {
            if (context?.LargeBatch == null)
            {
                return -1;
            }

            var totalSize = context.TotalThroughput;
            var batchSize = context.LargeBatch.BatchSize;

            return totalSize % batchSize == 0 ? totalSize / batchSize : (totalSize / batchSize) + 1;
        }

        /// <summary>
        /// 克隆 - 深复制
        /// </summary>
        public static ProcessingContext Copy(this ProcessingContext context)
        {
            return new ProcessingContext()
            {
                TotalThroughput = context.TotalThroughput,
                LargeBatch = context.LargeBatch,
                SmallBatch = context.SmallBatch
            };
        }
    }
}
