using System;

namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class ClientPostLogoutRedirectUriOutput
    {
        public Guid ClientId { get; set; }

        public string PostLogoutRedirectUri { get; set; }
    }
}