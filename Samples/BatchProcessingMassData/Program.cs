using BatchProcessingEngine;
using BatchProcessingEngine.WorkPool;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using BatchProcessingEngine.Extensions;

namespace BatchProcessingMassData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = ProcessingServiceProvider
                .GetServiceProvider();

            var executor = container.GetRequiredService<IExecutor>();

            var engine = new ProcessingEngineBuilder()
                .UseExecutor(executor)
                .Configure(app => { })
                .Build();

            await engine.StartAsync();

            Console.Read();
        }
    }
}
