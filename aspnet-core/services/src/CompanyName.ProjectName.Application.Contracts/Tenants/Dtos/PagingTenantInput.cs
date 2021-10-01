using CompanyName.ProjectName.Extensions.Customs;

namespace CompanyName.ProjectName.Tenants.Dtos
{
    public class PagingTenantInput : PagingBase
    {
        public string Filter { get; set; }
    }
}