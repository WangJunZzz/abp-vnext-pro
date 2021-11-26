using System;

namespace Lion.AbpPro.IdentityServers.Dtos
{
    public class ApiResourceScopeOutput
    {
        public Guid ApiResourceId { get; set; }

        public string Scope { get; set; }
    }
}