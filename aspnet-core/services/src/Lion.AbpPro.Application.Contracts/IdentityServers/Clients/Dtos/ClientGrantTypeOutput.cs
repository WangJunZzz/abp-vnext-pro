using System;

namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class ClientGrantTypeOutput
    {
        public Guid ClientId { get; set; }

        public string GrantType { get; set; }
    }
}