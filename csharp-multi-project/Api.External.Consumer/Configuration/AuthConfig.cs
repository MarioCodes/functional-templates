namespace Api.Core.Configuration
{
    public class AuthConfig
    {
        public const string Section = "AuthConfig";

        public virtual string User { get; set; }
        public virtual string Password { get; set; }
    }

}
