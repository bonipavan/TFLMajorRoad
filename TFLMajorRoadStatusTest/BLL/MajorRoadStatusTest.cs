using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using TFLMajorRoadStatusBLL;
using TFLMajorRoadStatusBLL.ConfigModels;
using TFLMajorRoadStatusBLL.Models;
using TFLMajorRoadStatusTest.Utility;

namespace TFLMajorRoadStatusTest.BLL
{
    class MajorRoadStatusTest
    {
        private Mock<IRequestBuilder> _requesttBuilder;
        private Mock<ILogger<MajorRoadStatus>> _logger;
        private IOptionsMonitor<MajorRoadStatusConfig> _majorRoadStatusConfig;

        [SetUp]
        public void Setup()
        {
            var setup = new TestSetUp();
            _requesttBuilder = new Mock<IRequestBuilder>();
            _logger = new Mock<ILogger<MajorRoadStatus>>();
            _majorRoadStatusConfig = setup._majorRoadStatusConfig;
        }

        [TestCase(TestConstants.argsNull)]
        [TestCase(TestConstants.argEmpty)]
        [TestCase(TestConstants.argValidRoad)]
        [TestCase(TestConstants.inValidRoadArgsType)]
        [TestCase(TestConstants.errorRoadArgsType)]
        public void GetMajorRoadStatusTest(string argsType)
        {
            //Arrange
            int expectedResponse = -1;            
            string[] args = null;
            switch (argsType)
            {
                case TestConstants.argEmpty:
                    {
                        // Empty string is sent arguments
                        args = new string[] { };
                    }
                    break;
                case TestConstants.argValidRoad:
                    {
                        // Invalid road name sent in args
                        args = new string[] { TestConstants.argValidRoad };
                        expectedResponse = 0;
                        _requesttBuilder.Setup(x => x.Send(It.IsAny<HttpMethod>(), It.IsAny<string>())).
                            Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(TestConstants.roadResponseContent) });
                    }
                    break;
                case TestConstants.inValidRoadArgsType:
                    {
                        // Valid Road name sent in args
                        args = new string[] { TestConstants.argInvalidRoad };
                        expectedResponse = 1;
                        _requesttBuilder.Setup(x => x.Send(It.IsAny<HttpMethod>(), It.IsAny<string>())).
                            Returns(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound, Content = new StringContent(TestConstants.roadResponseContent) });
                    }
                    break;
                case TestConstants.errorRoadArgsType:
                    {
                        // Throw Exception 
                        args = new string[] { TestConstants.argInvalidRoad };
                        expectedResponse = -1;
                        _requesttBuilder.Setup(x => x.Send(It.IsAny<HttpMethod>(), It.IsAny<string>())).
                            Throws(new Exception(TestConstants.testException));
                    }
                    break;
                default:
                    // send null in args
                    args = null;
                    break;
            }

            // Act 
            var majorRoadStatus = new MajorRoadStatus(_logger.Object, _majorRoadStatusConfig, _requesttBuilder.Object);
            var actualResponse = majorRoadStatus.GetMajorRoadStatus(args);

            // Assert
            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestCase]
        public void ValidRoadStatusMessageTest()
        {
            var status = new List<RoadStatusResponse>
          {
            new RoadStatusResponse
            {
              DisplayName = TestConstants.roadDpName,
              StatusSeverity = TestConstants.statusSeverity,
              StatusSeverityDescription = TestConstants.StatusSeverityDescription,
              Id = TestConstants.Id
            }
          };

            // Act 
            var majorRoadStatus = new MajorRoadStatus(_logger.Object, _majorRoadStatusConfig, _requesttBuilder.Object);
            var actualMessage = majorRoadStatus.ValidRoadStatusMessage(status);
            Assert.AreEqual(actualMessage, TestConstants.expectedMessage);
        }
    }

}
