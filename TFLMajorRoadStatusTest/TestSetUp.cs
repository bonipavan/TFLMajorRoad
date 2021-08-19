using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using TFLMajorRoadStatusBLL.ConfigModels;

namespace TFLMajorRoadStatusTest
{
    class TestSetUp
    {
        public IOptionsMonitor<MajorRoadStatusConfig> _majorRoadStatusConfig;
        public TestSetUp()
        {
            var config = InitConfiguration();
            var settings = new MajorRoadStatusConfig()
            {
                Url = config["MajorRoadStatusConfig:Url"],
                AppId = config["MajorRoadStatusConfig:AppId"],
                AppKey = config["MajorRoadStatusConfig:AppKey"]
            };

            _majorRoadStatusConfig  = Mock.Of<IOptionsMonitor<MajorRoadStatusConfig>>(_ => _.CurrentValue == settings);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
              .AddJsonFile("AppSettingstest.json")
              .Build();
            return config;
        }
    }
}
