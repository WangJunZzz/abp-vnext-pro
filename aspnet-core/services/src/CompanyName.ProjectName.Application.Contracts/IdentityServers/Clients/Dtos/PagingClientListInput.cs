using CompanyName.ProjectName.Extension.Customs.Dtos;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class PagingClientListInput:PagingBase
    {
        public string Filter { get; set; }
    }
}