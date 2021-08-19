using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TFLMajorRoadStatusBLL;
using TFLMajorRoadStatusBLL.ConfigModels;
using TFLMajorRoadStatusTest.Utility;

namespace TFLMajorRoadStatusTest.BLL
{
    class RequestBuilderTest
    {
        private HttpClient _httpClient;
        private Mock<HttpMessageHandler> mockResponseMessageHandler;
        private IOptionsMonitor<MajorRoadStatusConfig> _majorRoadStatusConfig;

        [SetUp]
        public void Setup()
        {
            var setup = new TestSetUp();
            _majorRoadStatusConfig = setup._majorRoadStatusConfig;
        }

        [TestCase(TestConstants.argValidRoad)]
        [TestCase(TestConstants.inValidRoadArgsType)]
        [TestCase(TestConstants.errorRoadArgsType)]
        public void SendTest(string roadType) 
        {
            // Arrange
            HttpStatusCode expectedHttpCode;
            string expectedContent;

            switch (roadType)
            {
                case TestConstants.argValidRoad:
                    {
                        // valid road name sent
                        expectedHttpCode = HttpStatusCode.OK;
                        expectedContent = TestConstants.roadResponseContent;
                    }
                    break;
                case TestConstants.inValidRoadArgsType:
                    {
                        // invalid road name sent
                        expectedHttpCode = HttpStatusCode.NotFound;
                        expectedContent = TestConstants.invalidRoadResponseContent;
                    }
                    break;
                default:
                    // error in getting road details
                    expectedHttpCode = HttpStatusCode.InternalServerError;
                    expectedContent = TestConstants.errorResponseContent;
                    break;
            }
                    mockResponseMessageHandler = new Mock<HttpMessageHandler>();
            mockResponseMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(TestConstants.sendAsync, ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = expectedHttpCode,
                    Content = new StringContent(expectedContent)
                });
            _httpClient = new HttpClient(mockResponseMessageHandler.Object);

            // Act
            var requestBuilder = new RequestBuilder(_httpClient);
            var actualResponse = requestBuilder.Send(HttpMethod.Get, _majorRoadStatusConfig.CurrentValue.Url);
            var actualResponseHttpCode = actualResponse.StatusCode;
            var actualResponseContent = actualResponse.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.AreEqual(actualResponseHttpCode, expectedHttpCode);
            Assert.AreEqual(actualResponseContent, expectedContent);
        }
    }
}
