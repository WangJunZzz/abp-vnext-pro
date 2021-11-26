using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.IdentityServers.ApiScopes.Dtos
{
    public class PagingApiScopeListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}