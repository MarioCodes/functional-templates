namespace Api.External.Consumer.Model
{
    public class WeekAvailabilityDTO
    {
        public FacilityDTO Facility { get; set; }
        public int SlotDurationMinutes { get; set; }
        public DayDTO? Monday { get; set; }
        public DayDTO? Tuesday { get; set; }
        public DayDTO? Wednesday { get; set; }
        public DayDTO? Thursday { get; set; }
        public DayDTO? Friday { get; set; }
        public DayDTO? Saturday { get; set; }
        public DayDTO? Sunday { get; set; }
    }

    public class BusySlotDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class FacilityDTO
    {
        public string FacilityId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class DayDTO
    {
        public WorkPeriodDTO WorkPeriod { get; set; }
        public List<BusySlotDTO> BusySlots { get; set; }
    }

    public class WorkPeriodDTO
    {
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public int LunchStartHour { get; set; }
        public int LunchEndHour { get; set; }
    }


}
