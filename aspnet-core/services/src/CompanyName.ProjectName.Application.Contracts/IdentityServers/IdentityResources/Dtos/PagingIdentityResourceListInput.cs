using CompanyName.ProjectName.Extensions.Customs;

namespace CompanyName.ProjectName.IdentityServers.IdentityResources.Dtos
{
    public class PagingIdentityResourceListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}