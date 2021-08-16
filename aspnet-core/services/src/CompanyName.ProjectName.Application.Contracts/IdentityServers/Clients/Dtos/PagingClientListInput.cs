using CompanyName.ProjectName.Extensions.Customs;

namespace CompanyName.ProjectName.IdentityServers.Clients
{
    public class PagingClientListInput:PagingBase
    {
        public string Filter { get; set; }
    }
}