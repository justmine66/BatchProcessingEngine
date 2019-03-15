using BatchProcessingEngine;
using BatchProcessingEngine.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BatchETL
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection container)
        {
            container.AddProcessingServices();

            container.AddScoped<IDataProvider, DataProvider>();
        }
    }
}
