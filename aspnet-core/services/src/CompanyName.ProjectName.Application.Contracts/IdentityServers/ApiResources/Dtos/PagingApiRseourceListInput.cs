using CompanyName.ProjectName.Extensions.Customs;

namespace CompanyName.ProjectName.IdentityServers.Dtos
{
    public class PagingApiRseourceListInput : PagingBase
    {
        public string Filter { get; set; }
    }
}