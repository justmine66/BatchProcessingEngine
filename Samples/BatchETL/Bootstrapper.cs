﻿using BatchProcessingEngine;
using BatchProcessingEngine.Eventting;
using BatchProcessingEngine.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BatchETL
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection container)
        {
            container.AddProcessingServices();

            container.AddSingleton<IDataProvider, MicroBatchDataProvider>();
            container.AddSingleton<IApplicationListener, ProcessingEngineCompletedNotifier>();
        }
    }
}
