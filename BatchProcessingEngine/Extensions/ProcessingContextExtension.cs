﻿namespace BatchProcessingEngine.Extensions
{
    public static class ProcessingContextExtension
    {
        /// <summary>
        /// 获取并行处理轮数
        /// </summary>
        /// <param name="context">处理上下文</param>
        public static int GetParallelRounds(this ProcessingContext context)
        {
            if (context?.MetaData.LargeBatch == null)
            {
                return -1;
            }

            var totalSize = context.MetaData.TotalThroughput;
            var batchSize = context.MetaData.LargeBatch.BatchSize;

            return totalSize % batchSize == 0 ? totalSize / batchSize : (totalSize / batchSize) + 1;
        }

        /// <summary>
        /// 克隆 - 深复制
        /// </summary>
        public static ProcessingContext Copy(this ProcessingContext context)
        {
            return new ProcessingContext()
            {
                MetaData = new ProcessingMetaData()
                {
                    TotalThroughput = context.MetaData.TotalThroughput,
                    LargeBatch = context.MetaData.LargeBatch,
                    SmallBatch = context.MetaData.SmallBatch
                },
            };
        }
    }
}
