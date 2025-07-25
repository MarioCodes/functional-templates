using Api.Core.Configuration;
using Api.Core.Models;
using Api.Core.Services.interfaces;
using Api.External.Consumer.Model;
using Api.External.Consumer.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class SlotsService(IExternalApiService _externalApiService,
        IOptions<ExternalApiConfig> _iOptExternalConfig) : ISlotsService
    {
        private ExternalApiConfig _config => _iOptExternalConfig.Value;

        public async Task<WeekAvailabilityResponse> GetWeekFreeSlotsAsync(DateOnly date)
        {
            var externalWeekData = await _externalApiService.GetWeeklyAvailabilityAsync(date);

            if (externalWeekData is null)
                throw new InvalidOperationException($"{_config.InvalidDataFromExternalApiError} for date {date.ToString()}");

            var weekAvailability = GetWeekPlanning(date, externalWeekData);
            weekAvailability.Facility = GetFacilityData(externalWeekData);
            return weekAvailability;
        }

        public async Task<string> ReserveSlotAsync(ReserveSlotRequest request)
        {
            // I do this mapping to decouple input data from data I send to the external API as it may change over time and we may need to adapt to it
            var mappedReserveSlot = MapReserveSlot(request);
            return await _externalApiService.ReserveSlotAsync(mappedReserveSlot);
        }

        private ReserveSlotDTO MapReserveSlot(ReserveSlotRequest request)
        {
            return new ReserveSlotDTO
            {
                FacilityId = request.FacilityId,
                Comments = request.Comments,
                Start = request.Start,
                End = request.End,
                Patient = new PatientDTO
                {
                    Email = request.Patient.Email,
                    Name = request.Patient.Name,
                    SecondName = request.Patient.SecondName,
                    Phone = request.Patient.Phone,
                }
            };
        }

        private Facility GetFacilityData(WeekAvailabilityDTO externalData)
        {
            var facility = externalData.Facility;
            return new Facility
            {
                Name = facility?.Name ?? "",
                Address = facility?.Address ?? ""
            };
        }

        private WeekAvailabilityResponse GetWeekPlanning(DateOnly date, WeekAvailabilityDTO externalWeekData) 
        {
            var weeklyPlanning = new WeekAvailabilityResponse();
            int duration = externalWeekData.SlotDurationMinutes;
            
            weeklyPlanning.Monday = GetDayPlanning(date.AddDays(0), externalWeekData.Monday, duration);
            weeklyPlanning.Tuesday = GetDayPlanning(date.AddDays(1), externalWeekData.Tuesday, duration);
            weeklyPlanning.Wednesday = GetDayPlanning(date.AddDays(2), externalWeekData.Wednesday, duration);
            weeklyPlanning.Thursday = GetDayPlanning(date.AddDays(3), externalWeekData.Thursday, duration);
            weeklyPlanning.Friday = GetDayPlanning(date.AddDays(4), externalWeekData.Friday, duration);
            weeklyPlanning.Saturday = GetDayPlanning(date.AddDays(5), externalWeekData.Saturday, duration);
            weeklyPlanning.Sunday = GetDayPlanning(date.AddDays(6), externalWeekData.Sunday, duration);

            return weeklyPlanning;
        }

        private Day? GetDayPlanning(DateOnly date, DayDTO? daySchedule, int slotDuration)
        {
            Day day = new Day();

            var daySlots = GetDaySlots(date.AddDays(0), daySchedule, slotDuration);
            if(daySlots.Any())
            {
                day.AvailableSlots = daySlots;
                return day;
            }

            // if I didn't receive data for a given day this may return null (Task) so the serializer automatically hides all nulls on response (see WeekAvailabilityDTO [JsonIgnore] tag)
            return null;
        }

        private List<AvailableSlot> GetDaySlots(DateOnly inputDay, DayDTO? daySchedule, int slotDuration)
        {
            if (daySchedule is null)
                return new List<AvailableSlot>();

            List<AvailableSlot> availableSlots = [];
            DateTime workshiftStart = ConvertToDateTime(inputDay, daySchedule.WorkPeriod.StartHour);
            DateTime workshiftEnd = ConvertToDateTime(inputDay, daySchedule.WorkPeriod.EndHour);

            DateTime lunchStart = ConvertToDateTime(inputDay, daySchedule.WorkPeriod.LunchStartHour);
            DateTime lunchEnd = ConvertToDateTime(inputDay, daySchedule.WorkPeriod.LunchEndHour);

            DateTime slotStart = workshiftStart;
            while (slotStart < workshiftEnd)
            {
                DateTime slotEnd = slotStart.AddMinutes(slotDuration);

                if (ItsLunchtime(lunchStart, lunchEnd, slotStart))
                {
                    // for cases where lunch duration is > 1 hour
                    int lunchTimeDuration =  lunchEnd.Hour - lunchStart.Hour;
                    slotStart = slotStart.AddHours(lunchTimeDuration);
                    // I also reset minutes for cases where we have a slot duration such as 45, so slots start again right after lunchtime
                    slotStart = slotStart.AddMinutes(-slotStart.Minute);
                    continue;
                }

                if (IsSlotFree(daySchedule, slotStart, slotEnd))
                {
                    availableSlots.Add(new AvailableSlot { StartTime = slotStart, EndTime = slotEnd });
                }

                slotStart = slotEnd;
            }

            return availableSlots;
        }

        private bool IsSlotFree(DayDTO daySchedule, DateTime slotStart, DateTime slotEnd)
        {
            bool isSlotBusy = daySchedule.BusySlots?.Any(busy => slotStart < busy.End && slotEnd > busy.Start) ?? false;
            return !isSlotBusy;
        }

        private bool ItsLunchtime(DateTime lunchStart, DateTime lunchEnd, DateTime slotStart)
        {
            return slotStart.Hour >= lunchStart.Hour && slotStart.Hour < lunchEnd.Hour;
        }

        private DateTime ConvertToDateTime(DateOnly dateOnly, int hour)
        {
            var time = new TimeOnly(hour, 0);
            return dateOnly.ToDateTime(time);
        }
    }
}
