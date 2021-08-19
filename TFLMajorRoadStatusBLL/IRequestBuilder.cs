using System.Net.Http;
namespace TFLMajorRoadStatusBLL
{
    public interface IRequestBuilder
    {
        public HttpResponseMessage Send(HttpMethod method, string url);
    }
}
