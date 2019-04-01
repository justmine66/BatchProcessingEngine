using BatchProcessingEngine;
using System;
using System.Threading.Tasks;

namespace BatchETL
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var container = ProcessingServiceProvider
                    .GetServiceProvider();

                var engine = new ProcessingEngineBuilder(container)
                    .ConfigureProcessingPipeLine(builder => builder.UserDataHandler())
                    .Build();

                await engine.StartAsync();

                Console.WriteLine(@"========================");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.Read();
        }
    }
}
