using BatchProcessingEngine.Notifications;
using System;

namespace BatchETL
{
    class ProcessingEngineCompletedNotifier : IApplicationListener
    {
        public void OnApplicationEvent(ApplicationEvent evt)
        {
            if (evt is ProcessingEngineCompletedEvent)
            {
                Console.WriteLine($"Processing Completed Notifier: {evt}");
            }
        }
    }
}
