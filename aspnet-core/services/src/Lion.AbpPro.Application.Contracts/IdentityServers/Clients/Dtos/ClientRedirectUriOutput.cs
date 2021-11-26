using System;
using Volo.Abp.Application.Dtos;
namespace Lion.AbpPro.IdentityServers.Clients
{
    public class ClientRedirectUriOutput
    {
        public virtual Guid ClientId { get; set; }

        public virtual string RedirectUri { get; set; }
    }
}