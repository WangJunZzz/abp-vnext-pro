using CompanyName.ProjectName.Extension.Customs.Dtos;

namespace CompanyName.ProjectName.Tenants.Dtos
{
    public class PagingTenantInput : PagingBase
    {
        public string Filter { get; set; }
    }
}