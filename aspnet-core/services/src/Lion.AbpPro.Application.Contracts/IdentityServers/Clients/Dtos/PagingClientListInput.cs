using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.IdentityServers.Clients.Dtos
{
    public class PagingClientListInput:PagingBase
    {
        public string Filter { get; set; }
    }
}