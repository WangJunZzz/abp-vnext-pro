using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.Roles.Dtos
{
    public class PagingRoleListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}