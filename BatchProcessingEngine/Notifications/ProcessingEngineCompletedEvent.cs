using System;

namespace BatchProcessingEngine.Notifications
{
    public class ProcessingEngineCompletedEvent : ApplicationEvent
    {
        public ProcessingEngineCompletedEvent(object source, int totalSize, TimeSpan elapsed)
            : base(source)
        {
            TotalSize = totalSize;
            ElapsedInSeconds = elapsed.TotalSeconds;
        }

        public int TotalSize { get; set; }

        public double ElapsedInSeconds { get; set; }

        public override string ToString()
        {
            return $"{{TotalSize: {TotalSize}, Elapsed: {ElapsedInSeconds}s}}";
        }
    }
}
