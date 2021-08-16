using System;
using Volo.Abp.Application.Dtos;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class ClientPropertyOutput
    {
        public Guid ClientId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}