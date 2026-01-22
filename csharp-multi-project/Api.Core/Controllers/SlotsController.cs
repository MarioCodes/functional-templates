using Api.Core.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Options;
using Api.Core.Configuration;
using Newtonsoft.Json;
using Api.Core.Models;

namespace Api.Core.Controllers
{
    [ApiController]
    [Route("slots")]
    public class SlotsController(ISlotsService _service,
        IOptions<CoreConfig> _iOptionsCoreConfig) : ControllerBase
    {

        private CoreConfig _coreConfig => _iOptionsCoreConfig.Value;

        /// <summary>
        /// Retrieves the availability of free slots for a given week based on the provided date.
        /// Input date must be a Monday, and it cannot be in the past.
        /// </summary>
        /// <param name="date">The date in string format to check availability for the week. The date must be formatted according to the input date format from configuration.</param>
        /// <returns>
        /// An object containing the week's availability if the input is valid.
        /// Returns a <see cref="BadRequestObjectResult"/> if the date is set in the past, is not a Monday, or has an incorrect format.
        /// </returns>
        /// <exception cref="Exception">Catches any unexpected exceptions and reuturns a <see cref="BadRequestObjectResult"/> with error details</exception>
        [HttpGet("/weeklyAvailability/{date}")]
        [SwaggerOperation(Tags = ["slots"])]
        public async Task<IActionResult> GetWeekAvailability(string date)
        {
            try
            {
                string inputDateFormat = _coreConfig.InputDateFormat;
                if(DateOnly.TryParseExact(date, inputDateFormat, out var parsedDate))
                {
                    if (parsedDate < DateOnly.FromDateTime(DateTime.Now))
                        return BadRequest(_coreConfig.ErrorMessages.InputDateSetInPast);

                    if (parsedDate.DayOfWeek != DayOfWeek.Monday)
                        return BadRequest(_coreConfig.ErrorMessages.InputDateNotMonday);

                    var response = await _service.GetWeekFreeSlotsAsync(parsedDate);
                    return Ok(response);
                }

                return BadRequest($"{_coreConfig.ErrorMessages.InputDateWrongFormat}: '{inputDateFormat}'");
            }
            catch (Exception ex)
            {
                return BadRequest($"{_coreConfig.ErrorMessages.InputDateGeneralError} for date {date} more info: {ex}");
            }
        }

        /// <summary>
        /// Reserves a slot based on the provided request details. 
        /// </summary>
        /// <param name="request">Input object with all details to reserve a slot of time for a patient in a clinic</param>
        /// <returns>
        /// An object containing an empty string and a 200 OK status if everything is reserved correctly 
        /// </returns>
        /// <exception cref="Exception">Catches any unexpected exceptions and returns a <see cref="BadRequestObjectResult"/> with error details</exception>
        [HttpPost("/reserveSlot")]
        [SwaggerOperation(Tags = ["slots"])]
        public async Task<IActionResult> ReserveSlot([FromBody] ReserveSlotRequest request)
        {
            try
            {
                var response = await _service.ReserveSlotAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{_coreConfig.ErrorMessages.ReserveSlotGeneralError} with data {JsonConvert.SerializeObject(request)} more info: {ex}");
            }
        }

    }
}
