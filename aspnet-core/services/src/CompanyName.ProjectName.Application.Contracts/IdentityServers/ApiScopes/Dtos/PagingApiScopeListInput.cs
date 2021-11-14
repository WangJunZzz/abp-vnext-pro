using CompanyName.ProjectName.Extension.Customs.Dtos;

namespace CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos
{
    public class PagingApiScopeListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}