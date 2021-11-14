using CompanyName.ProjectName.Extension.Customs.Dtos;

namespace CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos
{
    public class PagingIdentityResourceListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}