using Lion.Abp.Extension;

namespace CompanyName.ProjectName.IdentityServers.Dtos
{
        public class PagingApiRseourceListInput : PagingBase
        {
            public string Filter { get; set; }
        }
}