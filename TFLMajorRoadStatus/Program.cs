using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using TFLMajorRoadStatusBLL;
using TFLMajorRoadStatusBLL.ConfigModels;

namespace TFLMajorRoadStatus
{
    class Program
    {
        /// <summary>
        /// This methodThe starting point of the application
        /// </summary>
        /// <param name="args"> To take input parameters(Road Name) for the application</param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            ServiceProvider serviceProvider = ConfigureServices();
            return serviceProvider.GetService<IMajorRoadStatus>().GetMajorRoadStatus(args);
        }

        /// <summary>
        /// Configure the services like injecting dependencies
        /// </summary>
        /// <returns></returns>
        public static ServiceProvider ConfigureServices()
        {

            IConfiguration configuration = new ConfigurationBuilder().
              AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true).
              Build();

            var services = new ServiceCollection();

            services.Configure<MajorRoadStatusConfig>(configuration.GetSection("MajorRoadStatusConfig"));
            services.AddLogging(configure => configure.AddConsole());
            services.AddSingleton<HttpClient>();
            services.AddTransient<IRequestBuilder, RequestBuilder>();
            services.AddTransient<IMajorRoadStatus, MajorRoadStatus>();

            return services.BuildServiceProvider();
        }
    }
}
