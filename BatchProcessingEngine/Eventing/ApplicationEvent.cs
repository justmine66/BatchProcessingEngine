namespace BatchProcessingEngine.Eventing
{
    /// <summary>
    /// 表示一个应用程序事件
    /// </summary>
    public class ApplicationEvent
    {
        public ApplicationEvent(object source)
        {
            Source = source;
        }

        /// <summary>
        /// 事件源
        /// </summary>
        public object Source { get; set; }
    }
}
