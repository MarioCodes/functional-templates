using Api.External.Consumer.Model;

namespace Api.External.Consumer.Services.Interfaces
{
    public interface IExternalApiService
    {
        /// <summary>
        /// Retrieves weekly availability slots from the external API for the specified date.
        /// </summary>
        /// <param name="date">The date to use to retrieve all available slots.</param>
        /// <returns>A <see cref="WeekAvailabilityDTO"/> which has the available slots for the week or null if the response from the external API is empty or couldn't be deserialized.</returns>
        Task<WeekAvailabilityDTO?> GetWeeklyAvailabilityAsync(DateOnly date);

        /// <summary>
        /// Sends a request to reserve a slot via the external API.
        /// </summary>
        /// <param name="slotRequest">The <see cref="ReserveSlotDTO"/> object containing the details of the slot to be reserved.</param>
        /// <returns>A string which has the response from the external API.</returns>
        Task<string> ReserveSlotAsync(ReserveSlotDTO slotRequest);
    }
}
