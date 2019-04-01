using BatchProcessingEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using BatchProcessingEngine.Configuration;

namespace BatchETL
{
    public class ProcessingServiceProvider
    {
        public static IServiceProvider GetServiceProvider()
        {
            var env = Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(env))
                throw new Exception($"Tips: Please firstly set up the application environment[DOTNETCORE_ENVIRONMENT].");

            AppMode.Environment = env;

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{AppMode.Environment}.json")
                .Build();

            var container = new ServiceCollection()
                .AddLogging(logging =>
                {
                    logging.AddConfiguration(config.GetSection("Logging"));
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .AddSingleton<IConfiguration>(config);

            Bootstrapper.RegisterServices(container, config);

            return container.BuildServiceProvider(); 
        }
    }
}
