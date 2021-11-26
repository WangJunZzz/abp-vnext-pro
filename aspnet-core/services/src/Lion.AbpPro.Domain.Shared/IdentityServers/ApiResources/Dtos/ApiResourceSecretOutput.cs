using System;

namespace Lion.AbpPro.IdentityServers.Dtos
{
    public class ApiResourceSecretOutput
    {
        public Guid ApiResourceId { get; set; }

        public string Type { get;  set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public DateTime? Expiration { get; set; }
    }
}