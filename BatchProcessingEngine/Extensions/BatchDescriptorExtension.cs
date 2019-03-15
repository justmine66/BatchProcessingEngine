namespace BatchProcessingEngine.Extensions
{
    public static class BatchDescriptorExtension
    {
        public static int LargeBatchLastId(this BatchDescriptor largeBatch, int batchSize)
        {
            return largeBatch.CheckPointId + (largeBatch.BatchSequence - 1) * batchSize;
        }
    }
}
