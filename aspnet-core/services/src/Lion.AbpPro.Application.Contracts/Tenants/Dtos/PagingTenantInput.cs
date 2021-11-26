using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.Tenants.Dtos
{
    public class PagingTenantInput : PagingBase
    {
        public string Filter { get; set; }
    }
}