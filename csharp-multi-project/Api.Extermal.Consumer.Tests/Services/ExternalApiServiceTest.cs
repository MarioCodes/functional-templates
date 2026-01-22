using Api.Core.Configuration;
using Api.External.Consumer.Common.Interfaces;
using Api.External.Consumer.Model;
using Api.External.Consumer.Services;
using Api.External.Consumer.Services.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace Api.External.Consumer.Tests.Services
{
    public class ExternalApiServiceTest
    {
        private IExternalApiService _service;

        private Mock<HttpClient> _httpClientMock;
        private Mock<IHttpService> _httpServiceMock;

        private Mock<IOptions<ExternalApiConfig>> _iOptionsMock;
        private Mock<ExternalApiConfig> _configMock;

        private const string EXTERNAL_API_RESPONSE_SIMULATION = """
            {
                "Facility": {
                    "FacilityId": "c015550a-7dac-4904-bd83-ef6b48756bb8",
                    "Name": "Las Palmeras",
                    "Address": "Plaza de la independencia 36, 38006 Santa Cruz de Tenerife"
                },
                "SlotDurationMinutes": 10,
                "Monday": {
                    "WorkPeriod": {
                        "StartHour": 9,
                        "EndHour": 17,
                        "LunchStartHour": 13,
                        "LunchEndHour": 14
                    }
                },
                "Wednesday": {
                    "WorkPeriod": {
                        "StartHour": 9,
                        "EndHour": 17,
                        "LunchStartHour": 13,
                        "LunchEndHour": 14
                    },
                    "BusySlots": [
                        {
                            "Start": "2024-11-06T11:10:00",
                            "End": "2024-11-06T11:20:00"
                        }
                    ]
                },
                "Friday": {
                    "WorkPeriod": {
                        "StartHour": 8,
                        "EndHour": 16,
                        "LunchStartHour": 13,
                        "LunchEndHour": 14
                    }
                }
            }
            """;

        [SetUp]
        public void SetUp()
        {
            _httpClientMock = new Mock<HttpClient>();
            _httpServiceMock = new Mock<IHttpService>();

            _configMock = new Mock<ExternalApiConfig>();
            _iOptionsMock = new Mock<IOptions<ExternalApiConfig>>();
            _iOptionsMock.Setup(s => s.Value).Returns(_configMock.Object);

            _service = new ExternalApiService(_httpClientMock.Object, _httpServiceMock.Object, _iOptionsMock.Object);
        }

        [Test]
        public async Task GivenJsonStructure_WhenGetAvailability_ThenResponseParsesData()
        {
            // given
            DateOnly dateMonday = new DateOnly(2024, 10, 02);

            string baseUrl = "http://base_url";
            string endpoint = "/this_is_an_endpoint/";
            string dateFormat = "yyyyMMdd";
            string dateMondayStr = "20241002";
            string fullUrlToCall = baseUrl + endpoint + dateMondayStr;

            _configMock.Setup(conf => conf.ExternalApiDateFormat).Returns(dateFormat);
            _configMock.Setup(conf => conf.BaseUrl).Returns(baseUrl);
            _configMock.Setup(conf => conf.AvailabilityEndpoint).Returns(endpoint);

            var httpReqMock = new Mock<HttpRequestMessage>(HttpMethod.Get, fullUrlToCall);
            _httpServiceMock.Setup(httpService => httpService.SetUpGet(fullUrlToCall)).Returns(httpReqMock.Object);
            _httpServiceMock.Setup(httpService => httpService.HttpCallAsync(_httpClientMock.Object, It.IsAny<Func<HttpRequestMessage>>())).ReturnsAsync(EXTERNAL_API_RESPONSE_SIMULATION);

            // when
            var response = await _service.GetWeeklyAvailabilityAsync(dateMonday);

            // then
            response.Should().NotBeNull();
            response.Facility.FacilityId.Should().Be("c015550a-7dac-4904-bd83-ef6b48756bb8");
            response.Facility.Name.Should().Be("Las Palmeras");
            response.Facility.Address.Should().Be("Plaza de la independencia 36, 38006 Santa Cruz de Tenerife");
            response.SlotDurationMinutes.Should().Be(10);

            response.Tuesday.Should().BeNull();
            response.Thursday.Should().BeNull();
            response.Saturday.Should().BeNull();
            response.Sunday.Should().BeNull();

            response.Monday.Should().NotBeNull();
            response.Wednesday.Should().NotBeNull();
            response.Friday.Should().NotBeNull();

            response.Monday.WorkPeriod.StartHour.Should().Be(9);
            response.Monday.WorkPeriod.EndHour.Should().Be(17);
            response.Monday.WorkPeriod.LunchStartHour.Should().Be(13);
            response.Monday.WorkPeriod.LunchEndHour.Should().Be(14);
        }

        [Test]
        public async Task GivenPayload_WhenReserveSlot_ThenResponseParsesData()
        {
            // given
            var request = new ReserveSlotDTO
            {
                FacilityId = "c015550a-7dac-4904-bd83-ef6b48756bb8",
                Start = "2024-11-04 09:00:00",
                End = "2024-11-04 09:10:00",
                Comments = "my knee hurts sometimes when it's about to rain",
                Patient = new PatientDTO
                {
                    Name = "Mario",
                    SecondName = "Neta",
                    Email = "mario.neta@example.com",
                    Phone = "+34617829923"
                }
            };

            string baseUrl = "http://base_url";
            string endpoint = "/this_is_an_endpoint/";
            string fullUrlToCall = baseUrl + endpoint;
            _configMock.Setup(conf => conf.BaseUrl).Returns(baseUrl);
            _configMock.Setup(conf => conf.AvailabilityEndpoint).Returns(endpoint);

            var httpReqMock = new Mock<HttpRequestMessage>(HttpMethod.Post, fullUrlToCall);
            _httpServiceMock.Setup(httpService => httpService.SetUpPost(fullUrlToCall, request)).Returns(httpReqMock.Object);
            _httpServiceMock.Setup(httpService => httpService.HttpCallAsync(_httpClientMock.Object, It.IsAny<Func<HttpRequestMessage>>())).ReturnsAsync("");

            // when
            var response = await _service.ReserveSlotAsync(request);

            // then
            response.Should().NotBeNull();
            response.Should().BeEmpty();
        }
    }
}
