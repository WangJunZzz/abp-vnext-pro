using System;

namespace Lion.AbpPro.IdentityServers.Dtos
{
    public class ApiResourceClaimOutput
    {
        public  Guid ApiResourceId { get; set; }
        
        public  string Type { get;  set; }
    }
}