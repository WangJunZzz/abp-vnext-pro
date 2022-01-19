using System;

namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class ClientCorsOriginOutput
    {
        public Guid ClientId { get; set; }

        public string Origin { get; set; }
    }
}