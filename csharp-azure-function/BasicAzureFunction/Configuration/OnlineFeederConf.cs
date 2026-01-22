namespace BasicAzureFunction.Options
{
    public class OnlineFeederConf
    {
        public string Url { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 60;
    }
}
