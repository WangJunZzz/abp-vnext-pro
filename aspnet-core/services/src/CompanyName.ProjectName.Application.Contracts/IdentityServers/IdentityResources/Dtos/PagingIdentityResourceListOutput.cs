using System;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos
{
    public class PagingIdentityResourceListOutput:EntityDto<Guid>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public bool Required { get; set; }

        public bool Emphasize { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
    }
}