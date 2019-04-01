using System;
using BatchProcessingEngine.Notifications;

namespace BatchETL
{
    class ProcessingEngineCompletedNotifier : IApplicationListener
    {
        public void OnApplicationEvent(ApplicationEvent evt)
        {
            if (evt is ProcessingEngineCompletedEvent)
            {
                Console.WriteLine($"{evt}");
            }
        }
    }
}
