using System;
using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.IdentityServers.Clients
{
    public class ClientCorsOriginOutput
    {
        public Guid ClientId { get; set; }

        public string Origin { get; set; }
    }
}