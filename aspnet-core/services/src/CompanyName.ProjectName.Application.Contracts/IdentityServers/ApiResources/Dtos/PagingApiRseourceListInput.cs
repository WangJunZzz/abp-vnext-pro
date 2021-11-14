using CompanyName.ProjectName.Extension.Customs.Dtos;

namespace CompanyName.ProjectName.IdentityServers.Dtos
{
        public class PagingApiRseourceListInput : PagingBase
        {
            public string Filter { get; set; }
        }
}