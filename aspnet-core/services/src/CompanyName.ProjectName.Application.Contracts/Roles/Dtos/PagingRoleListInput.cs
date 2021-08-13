using CompanyName.ProjectName.Extensions.Customs;
using CompanyName.ProjectName.Publics.Dtos;

namespace CompanyName.ProjectName.Roles.Dtos
{
    public class PagingRoleListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}