using Lion.Abp.Extension;

namespace CompanyName.ProjectName.Tenants.Dtos
{
    public class PagingTenantInput : PagingBase
    {
        public string Filter { get; set; }
    }
}