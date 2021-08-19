using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TFLMajorRoadStatusBLL.ConfigModels;
using TFLMajorRoadStatusBLL.Models;
using TFLMajorRoadStatusBLL.Utility;

namespace TFLMajorRoadStatusBLL
{
    public class MajorRoadStatus : IMajorRoadStatus
    {
        private readonly ILogger _logger;
        private readonly MajorRoadStatusConfig _majorRoadStatusConfig;
        private readonly IRequestBuilder _requestBuilder;
        public MajorRoadStatus(ILogger<MajorRoadStatus> logger, IOptionsMonitor<MajorRoadStatusConfig> majorRoadStatusConfig, IRequestBuilder requestBuilder)
        {
            _logger = logger;
            _majorRoadStatusConfig = majorRoadStatusConfig.CurrentValue;
            _requestBuilder = requestBuilder;
        }

        /// <summary>
        /// This methods is used to get the road staus of a raod name from tfl end point and prints the road statuses
        /// </summary>
        /// <param name="args">road name which is sent from coomand line arguments. </param>
        /// <returns>returns the status code of the road status</returns>
        public int GetMajorRoadStatus(string[] args)
        {
            string roadName = string.Empty;
            try
            {
                if (args.Length > 0)
                {
                    roadName = args[0];
                }
                if (!string.IsNullOrWhiteSpace(roadName))
                {
                    // Call request builder send method to send road name over http client and get response
                    var response = _requestBuilder.Send(HttpMethod.Get, CreateUri(roadName));
                    // If response returned with success status code
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responseContentString = response.Content.ReadAsStringAsync().Result;
                        if (!string.IsNullOrWhiteSpace(responseContentString))
                        {
                            var roadStatusResponses = JsonConvert.DeserializeObject<List<RoadStatusResponse>>(responseContentString);
                            // print the road status
                            Print(ValidRoadStatusMessage(roadStatusResponses));
                        }
                    }
                    // If response returned with not found status code
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        Print($"{roadName} {Constants.notValidMsg}");
                        return 1;
                    }
                    // If response returned with any other status code
                    else
                    {
                        Print($"{Constants.errorMsg} {roadName}");
                        return -1;
                    }
                }
                else
                {
                    Print(Constants.emptyRoadMsg);
                    return -1;
                }
            }
            catch (Exception)
            {
                // Developer purpose to identify the error by logging it .              
                //_logger.LogError($"{Constants.errorLogMsg} : {Constants.errorDetailsMsg} - {ex}");
                Print($"{Constants.errorMsg} {roadName}");
                return -1;
            }
            return 0;
        }

        private string CreateUri(string roadName)
        {
            return $"{_majorRoadStatusConfig.Url}{roadName}?{Constants.appIdString}={_majorRoadStatusConfig.AppId}&{Constants.appKeyString}={_majorRoadStatusConfig.AppKey}";
        }

        public string ValidRoadStatusMessage(List<RoadStatusResponse> roadStatusResponses)
        {
            var message = new StringBuilder();
            if (roadStatusResponses != null && roadStatusResponses.Any())
            {
                roadStatusResponses.ForEach(road => message.AppendLine($"{Constants.statOfRoadMsg} {road.DisplayName} {Constants.followMsg}")
                    .AppendLine($"\t{Constants.roadStatusMsg} {road.StatusSeverity}")
                    .AppendLine($"\t{Constants.roadStatDescMsg} {road.StatusSeverityDescription}"));
            }
            return message.ToString();
        }

        private void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
