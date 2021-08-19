using System;
using System.Net;
using System.Net.Http;
using TFLMajorRoadStatusBLL.Utility;

namespace TFLMajorRoadStatusBLL
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly HttpClient _httpClient;
        public RequestBuilder(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// This method is used to send the http request for the given url and fetches the response from it.
        /// </summary>
        /// <param name="method"> Http method type to send request</param>
        /// <param name="url"/> url to which end point request to be sent</param>
        /// <returns> returns back the response which is received from httpclient</returns>
        public HttpResponseMessage Send(HttpMethod method, string url)
        {
            WebRequest.DefaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
            var httpRequestMessage = new HttpRequestMessage(method, url);           
            _httpClient.DefaultRequestHeaders.ExpectContinue = true;
            _httpClient.Timeout = TimeSpan.FromSeconds(Constants.requestTimeOut);

            // send the request to TFL Road end point
            return _httpClient.SendAsync(httpRequestMessage).Result;
        }
    }
}
