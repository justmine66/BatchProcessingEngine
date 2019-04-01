namespace BatchProcessingEngine.Eventing
{
    /// <summary>
    /// 应用程序事件源
    /// </summary>
    public interface IApplicationListenerSource
    {
        void Publish(ApplicationEvent evt);
    }
}
