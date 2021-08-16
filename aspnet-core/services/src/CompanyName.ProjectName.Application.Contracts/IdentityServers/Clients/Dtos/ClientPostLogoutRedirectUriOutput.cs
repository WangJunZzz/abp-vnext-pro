using System;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class ClientPostLogoutRedirectUriOutput
    {
        public Guid ClientId { get; set; }

        public string PostLogoutRedirectUri { get; set; }
    }
}