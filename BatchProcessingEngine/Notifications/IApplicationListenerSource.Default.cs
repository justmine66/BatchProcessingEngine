using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace BatchProcessingEngine.Notifications
{
    public class ApplicationListenerSourceImpl : IApplicationListenerSource
    {
        private readonly IEnumerable<IApplicationListener> _listeners;

        public ApplicationListenerSourceImpl(IServiceProvider container)
        {
            _listeners = container.GetServices<IApplicationListener>();
        }

        public void Publish(ApplicationEvent evt)
        {
            Assert.NotNull(evt);

            foreach (var listener in _listeners)
            {
                listener.OnApplicationEvent(evt);
            }
        }
    }
}
