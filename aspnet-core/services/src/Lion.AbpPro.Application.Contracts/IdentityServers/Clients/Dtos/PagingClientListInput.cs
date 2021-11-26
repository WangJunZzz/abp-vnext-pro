using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.IdentityServers.Clients
{
    public class PagingClientListInput:PagingBase
    {
        public string Filter { get; set; }
    }
}