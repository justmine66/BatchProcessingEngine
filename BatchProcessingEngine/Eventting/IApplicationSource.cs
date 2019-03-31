namespace BatchProcessingEngine.Eventting
{
    /// <summary>
    /// 应用程序事件源
    /// </summary>
    public interface IApplicationSource
    {
        void Puslish(ApplicationEvent evt);
    }
}
