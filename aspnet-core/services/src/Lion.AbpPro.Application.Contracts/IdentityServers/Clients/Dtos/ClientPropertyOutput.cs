using System;

namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class ClientPropertyOutput
    {
        public Guid ClientId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}