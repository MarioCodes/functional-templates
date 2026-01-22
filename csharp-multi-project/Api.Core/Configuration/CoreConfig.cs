namespace Api.Core.Configuration
{
    public class CoreConfig
    {
        public const string Section = "CoreConfig";
        /// <summary>
        /// Defines the expected date format which this API expects. Right now the accepted format is yyyyMMdd.
        /// Each project has its own valid format as they may split in the future.
        /// </summary>
        public virtual string InputDateFormat { get; set; }
        public virtual ErrorMessages ErrorMessages { get; set; }
    }

    public class ErrorMessages
    {
        public virtual string InputDateSetInPast { get; set; }
        public virtual string InputDateNotMonday { get; set; }
        public virtual string InputDateWrongFormat { get; set; }
        public virtual string InputDateGeneralError { get; set; }
        public virtual string ReserveSlotGeneralError { get; set; }
    }
}
