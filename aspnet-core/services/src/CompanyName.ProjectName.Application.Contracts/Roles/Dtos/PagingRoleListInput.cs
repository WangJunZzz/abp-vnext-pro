using Lion.Abp.Extension;

namespace CompanyName.ProjectName.Roles.Dtos
{
    public class PagingRoleListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}