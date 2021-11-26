using System;
using Volo.Abp.Application.Dtos;

namespace Lion.AbpPro.IdentityServers.ApiScopes.Dtos
{
    public class PagingApiScopeListOutput : EntityDto<Guid>
    {
        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        public bool Emphasize { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
    }
}