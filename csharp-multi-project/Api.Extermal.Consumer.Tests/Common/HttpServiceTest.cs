using Api.Core.Configuration;
using Api.External.Consumer.Common;
using Api.External.Consumer.Common.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Api.External.Consumer.Tests.Common
{
    public class HttpServiceTest
    {
        private IHttpService _service;

        private Mock<IOptions<ExternalApiConfig>> _iOptionsApiConfigMock;
        private Mock<ExternalApiConfig> _apiConfigMock;
        
        private Mock<IOptions<AuthConfig>> _iOptionsAuthConfigMock;
        private Mock<AuthConfig> _authConfigMock;

        [SetUp]
        public void SetUp()
        {
            _authConfigMock = new Mock<AuthConfig>();
            _iOptionsAuthConfigMock = new Mock<IOptions<AuthConfig>>();
            _iOptionsAuthConfigMock.Setup(iOpt => iOpt.Value).Returns(_authConfigMock.Object);

            _apiConfigMock = new Mock<ExternalApiConfig>();
            _iOptionsApiConfigMock = new Mock<IOptions<ExternalApiConfig>>();
            _iOptionsApiConfigMock.Setup(iOpt => iOpt.Value).Returns(_apiConfigMock.Object);

            _service = new HttpService(_iOptionsApiConfigMock.Object, _iOptionsAuthConfigMock.Object);
        }

        [Test]
        public async Task GivenAuthDataAndUrl_WhenSetupGet_ThenReturnsContainsCorrectData()
        {
            // given
            string url = "http://somethingsomewhere/endpoint";

            string username = "mario";
            string password = "neta";
            _authConfigMock.Setup(conf => conf.User).Returns(username);
            _authConfigMock.Setup(conf => conf.Password).Returns(password);

            // this is just the encoding in base64 of "mario:neta" for testing purposes
            string base64Encoded = "bWFyaW86bmV0YQ==";

            // when
            HttpRequestMessage result = _service.SetUpGet(url);

            // then
            result.Should().NotBeNull();
            result.RequestUri.Should().Be(url);
            result.Headers.Authorization.Scheme.Should().Be("Basic");
            result.Headers.Authorization.Parameter.Should().Be(base64Encoded);
        }

        [Test]
        public async Task GivenAuthDataAndUrl_WhenSetupPost_ThenReturnsContainsCorrectData()
        {
            // given
            string url = "http://somethingsomewhere/endpoint";

            string username = "mario";
            string password = "neta";
            _authConfigMock.Setup(conf => conf.User).Returns(username);
            _authConfigMock.Setup(conf => conf.Password).Returns(password);

            // this is just the encoding in base64 of "mario:neta" for testing purposes
            string base64Encoded = "bWFyaW86bmV0YQ==";

            string bogusJsonPayload = """
                {
            	    "Monday": {
            		    "WorkPeriod": {
            			    "StartHour": 9,
            			    "EndHour": 17,
            			    "LunchStartHour": 13,
            			    "LunchEndHour": 14
            		    },
            		    "BusySlots": [
            			    {
            				    "Start": "2024-10-07T12:20:00",
            				    "End": "2024-10-07T12:30:00"
            			    }
            		    ]
            	    }
                }
            """;

            // when
            HttpRequestMessage result = _service.SetUpPost(url, bogusJsonPayload);

            // then
            result.Should().NotBeNull();
            result.RequestUri.Should().Be(url);
            result.Headers.Authorization.Scheme.Should().Be("Basic");
            result.Headers.Authorization.Parameter.Should().Be(base64Encoded);
            result.Content.Should().NotBeNull();
        }

        [Test]
        public async Task GivenNullPayload_WhenSetupPost_ThenThrowsExceptionWithCorrectTypeAndMessage()
        {
            // given
            string url = "http://somethingsomewhere/endpoint";

            string username = "mario";
            string password = "neta";
            _authConfigMock.Setup(conf => conf.User).Returns(username);
            _authConfigMock.Setup(conf => conf.Password).Returns(password);

            // this is just the encoding in base64 of "mario:neta" for testing purposes
            string base64Encoded = "bWFyaW86bmV0YQ==";

            string nullPayload = null;

            // when
            Action result = () => _service.SetUpPost(url, nullPayload);

            // then
            result.Should().Throw<ArgumentNullException>().WithMessage("*null payload*");
        }

    }
}
