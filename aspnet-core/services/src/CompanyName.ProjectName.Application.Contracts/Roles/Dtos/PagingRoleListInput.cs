using CompanyName.ProjectName.Extension.Customs.Dtos;

namespace CompanyName.ProjectName.Roles.Dtos
{
    public class PagingRoleListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}