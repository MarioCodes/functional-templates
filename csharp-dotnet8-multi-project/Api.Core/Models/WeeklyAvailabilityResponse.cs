using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Core.Models
{
    public class WeekAvailabilityResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Facility Facility { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Day? Monday { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Day? Tuesday { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Day? Wednesday { get;set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Day? Thursday { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Day? Friday { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Day? Saturday { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Day? Sunday { get; set; }
    }

    public class Facility
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Day
    {
        public List<AvailableSlot> AvailableSlots { get; set; } = new List<AvailableSlot>();
    }

    public class AvailableSlot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
