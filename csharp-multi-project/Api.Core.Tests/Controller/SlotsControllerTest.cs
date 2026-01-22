using Api.Core.Configuration;
using Api.Core.Controllers;
using Api.Core.Models;
using Api.Core.Services.interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace Api.Core.Tests.Controller
{
    public class SlotsControllerTest
    {
        private SlotsController _controller;

        private Mock<ISlotsService> _slotsServiceMock;

        private Mock<IOptions<CoreConfig>> _iOptionsCoreConfigMock;
        private Mock<CoreConfig> _coreConfigMock;

        [SetUp]
        public void SetUp()
        {
            _coreConfigMock = new Mock<CoreConfig>();
            _iOptionsCoreConfigMock = new Mock<IOptions<CoreConfig>>();
            _iOptionsCoreConfigMock.Setup(iOpt => iOpt.Value).Returns(_coreConfigMock.Object);

            _slotsServiceMock = new Mock<ISlotsService>();
            _controller = new SlotsController(_slotsServiceMock.Object, _iOptionsCoreConfigMock.Object);
        }

        [Test]
        public async Task GivenEmptyStringValidation_WhenGetWeekAvailability_ThenAssert400ReturnValueWithMessage()
        {
            // given
            string date = "";
            string dateFormat = "yyyyMMdd";
            string errorMessage = "date has wrong format";

            var errorMessages = new ErrorMessages
            {
                InputDateWrongFormat = errorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            // when
            var result = await _controller.GetWeekAvailability(date) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(errorMessage);
        }

        [Test]
        public async Task GivenNullStringValidation_WhenGetWeekAvailability_ThenAssert400ReturnValueWithMessage()
        {
            // given
            string errorMessage = "date has wrong format";
            string dateFormat = "yyyyMMdd";

            var errorMessages = new ErrorMessages
            {
                InputDateWrongFormat = errorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            // when
            var result = await _controller.GetWeekAvailability(null) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(errorMessage);
        }

        [Test]
        public async Task GivenAlphaNumericStringValidation_WhenGetWeekAvailability_ThenAssert400ReturnValueWithMessage()
        {
            // given
            string date = "ThisIsNoDate1234";
            string errorMessage = "date has wrong format";
            string dateFormat = "yyyyMMdd";

            var errorMessages = new ErrorMessages
            {
                InputDateWrongFormat = errorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            // when
            var result = await _controller.GetWeekAvailability(date) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(errorMessage);
        }

        [Test]
        public async Task GivenWrongFormatDateValidation_WhenGetWeekAvailability_ThenAssert400ReturnValueWithMessage()
        {
            // given
            string date = "22/11/2024";
            string errorMessage = "date has wrong format";
            string dateFormat = "yyyyMMdd";

            var errorMessages = new ErrorMessages
            {
                InputDateWrongFormat = errorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            // when
            var result = await _controller.GetWeekAvailability(date) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(errorMessage);
        }

        [Test]
        public async Task GivenPastDateValidation_WhenGetWeekAvailability_ThenAssert400ReturnValueWithMessage()
        {
            // given
            string date = "20220606";
            string errorMessage = "date is set in the past. no past dates allowed";
            string dateFormat = "yyyyMMdd";

            var errorMessages = new ErrorMessages
            {
                InputDateSetInPast = errorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            // when
            var result = await _controller.GetWeekAvailability(date) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(errorMessage);
        }

        [Test]
        public async Task GivenDateIsNoMondayValidation_WhenGetWeekAvailability_ThenAssert400ReturnValueWithMessage()
        {
            // given
            string date = "20241215";
            string errorMessage = "date is not a monday. only mondays are allowed";
            string dateFormat = "yyyyMMdd";

            var errorMessages = new ErrorMessages
            {
                InputDateNotMonday = errorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            // when
            var result = await _controller.GetWeekAvailability(date) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(errorMessage);
        }

        [Test]
        public async Task GivenServiceThrowsException_WhenGetWeekAvailability_ThenAssertExceptionIsCaught()
        {
            // given
            string date = "20241216";
            DateOnly parsedDate = new DateOnly(2024, 12, 16);

            string outputErrorMessage = "an error has ocurred";
            string dateFormat = "yyyyMMdd";

            var errorMessages = new ErrorMessages
            {
                InputDateGeneralError = outputErrorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            _slotsServiceMock.Setup(service => service.GetWeekFreeSlotsAsync(parsedDate))
                .ThrowsAsync(new InvalidOperationException("error message from exception"));

            // when
            var result = await _controller.GetWeekAvailability(date) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(outputErrorMessage);
        }

        [Test]
        public async Task GivenCorrectDate_WhenGetWeekAvailability_ThenAssertCorrectValueIsReturned()
        {
            // given
            string date = "20241216";
            DateOnly parsedDate = new DateOnly(2024, 12, 16);

            string dateFormat = "yyyyMMdd";
            _coreConfigMock.Setup(conf => conf.InputDateFormat).Returns(dateFormat);

            WeekAvailabilityResponse dtoResponse = new WeekAvailabilityResponse();

            _slotsServiceMock.Setup(service => service.GetWeekFreeSlotsAsync(parsedDate))
                .ReturnsAsync(dtoResponse);

            // when
            var result = await _controller.GetWeekAvailability(date) as OkObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be(dtoResponse);
        }

        [Test]
        public async Task GivenServiceThrowsException_WhenReserveSlot_ThenAssertExceptionIsCaught()
        {
            // given
            var request = new ReserveSlotRequest
            {
                FacilityId = "c015550a-7dac-4904-bd83-ef6b48756bb8",
                Start = "2024-11-04 09:00:00",
                End = "2024-11-04 09:10:00",
                Comments = "my knee hurts sometimes when it's about to rain",
                Patient = new Patient
                {
                    Name = "Mario",
                    SecondName = "Neta",
                    Email = "mario.neta@example.com",
                    Phone = "+34617829923"
                }
            };

            string outputErrorMessage = "an error has ocurred";
            var errorMessages = new ErrorMessages
            {
                ReserveSlotGeneralError = outputErrorMessage
            };

            _coreConfigMock.Setup(conf => conf.ErrorMessages).Returns(errorMessages);

            _slotsServiceMock.Setup(service => service.ReserveSlotAsync(request))
                .ThrowsAsync(new HttpRequestException("error message from exception"));

            // when
            var result = await _controller.ReserveSlot(request) as BadRequestObjectResult;

            // then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
            result.Value.ToString().Should().Contain(outputErrorMessage);
        }

    }
}
