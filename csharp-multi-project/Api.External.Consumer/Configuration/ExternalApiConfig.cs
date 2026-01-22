namespace Api.Core.Configuration
{
    public class ExternalApiConfig
    {
        public const string Section = "ExternalApiConfig";

        public virtual string BaseUrl { get; set; }
        public virtual string AvailabilityEndpoint { get; set; }
        public string TakeSlotEndpoint { get; set; }
        public virtual int RetryTimespanInSeconds { get; set; }
        public virtual int RetryAttempts { get; set; }
        public virtual string ExternalApiDateFormat { get; set; }
        public virtual string InvalidDataFromExternalApiError { get; set; }
    }

}
