using Api.Core.Models;
using System;
using System.Threading.Tasks;

namespace Api.Core.Services.interfaces
{
    public interface ISlotsService
    {
        /// <summary>
        /// Retrieves the available free slots for a specific week based on the provided date.
        /// </summary>
        /// <param name="date">The date used to retrieve the free slots for the week.</param>
        /// <returns>
        ///     A <see cref="WeekAvailabilityResponse"/> object containing the week's availability.
        /// </returns>
        Task<WeekAvailabilityResponse> GetWeekFreeSlotsAsync(DateOnly date);
        
        /// <summary>
        /// Reserves a slot using the provided request data.
        /// </summary>
        /// <param name="request">An object which holds reservation details.</param>
        /// <returns>
        /// A string representing the response from the external API after attempting to reserve the slot.
        /// </returns>
        Task<string> ReserveSlotAsync(ReserveSlotRequest request);

    }
}
