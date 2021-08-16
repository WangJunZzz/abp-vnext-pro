using System;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class ClientIdPRestrictionOutput
    {
        public Guid ClientId { get; set; }

        public string Provider { get; set; }
    }
}