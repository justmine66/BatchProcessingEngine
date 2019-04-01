namespace BatchProcessingEngine.Notifications
{
    /// <summary>
    /// 应用程序事件监听器
    /// </summary>
    public interface IApplicationListener
    {
        void OnApplicationEvent(ApplicationEvent evt);
    }
}
