using Lion.Abp.Extension;

namespace CompanyName.ProjectName.IdentityServers.ApiScopes.Dtos
{
    public class PagingApiScopeListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}