using System;
using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.IdentityServers.Clients
{
    public class ClientPostLogoutRedirectUriOutput
    {
        public Guid ClientId { get; set; }

        public string PostLogoutRedirectUri { get; set; }
    }
}