﻿using BatchProcessingEngine.WorkPool;
using Microsoft.Extensions.DependencyInjection;

namespace BatchProcessingEngine.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProcessingServices(this IServiceCollection container)
        {
            container.AddSingleton<IEngine, ProcessingEngine>();
            container.AddSingleton<IScheduler, ProcessingScheduler>();

            container.AddScoped<IExecutor, BasicExecutor>();
            container.AddScoped<IProcessor, BatchProcessor>();

            container.AddScoped<IMiddlewareFactory, MiddlewareFactory>();

            return container;
        }
    }
}