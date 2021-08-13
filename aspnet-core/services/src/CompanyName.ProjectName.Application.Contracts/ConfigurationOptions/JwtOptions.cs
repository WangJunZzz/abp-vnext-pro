namespace CompanyName.ProjectName.ConfigurationOptions
{
    public class JwtOptions
    {
        public int ExpirationTime { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public string Issuer { get; set; }
    }
}