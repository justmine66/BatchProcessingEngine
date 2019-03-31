using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BatchProcessingEngine.Eventting
{
    public class ApplicationSourceImpl : IApplicationSource
    {
        private readonly IServiceProvider _container;
        private readonly IEnumerable<IApplicationListener> _listeners;

        public ApplicationSourceImpl(IServiceProvider container, IApplicationListener listener)
        {
            _container = container;
            _listeners = _container.GetServices<IApplicationListener>();
        }

        public void Puslish(ApplicationEvent evt)
        {
            Assert.NotNull(evt);

            foreach (var listener in _listeners)
            {
                listener.OnApplicationEvent(evt);
            }
        }
    }
}
