﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BatchProcessingEngine.Configuration
{
    public static class BatchProcessingEngineBuilderExtension
    {
        public static IBatchProcessingEngineBuilder SetBatchProcessingEngine(this IBatchProcessingEngineBuilder builder, float batchProcessingFactor = 1f, float microBatchProcessingFactor = 0.2f)
        {
            builder.Services.Add(ServiceDescriptor.Singleton<IConfigureOptions<ProcessingOptions>>(
                new ConfigureProcessingOptions(batchProcessingFactor, microBatchProcessingFactor)));
            return builder;
        }
    }
}
