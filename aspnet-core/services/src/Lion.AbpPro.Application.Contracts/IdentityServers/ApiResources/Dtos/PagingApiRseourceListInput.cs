using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.IdentityServers.ApiResources.Dtos
{
        public class PagingApiRseourceListInput : PagingBase
        {
            public string Filter { get; set; }
        }
}