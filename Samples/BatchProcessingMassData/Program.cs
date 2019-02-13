using BatchProcessingEngine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BatchProcessingMassData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var engine = ProcessingServiceProvider
                    .GetServiceProvider()
                    .GetRequiredService<IEngine>();

                await engine.StartAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
