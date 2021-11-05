using Lion.Abp.Extension;

namespace CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos
{
    public class PagingIdentityResourceListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}