using System;
using CompanyName.ProjectName.Extensions.Customs;

namespace CompanyName.ProjectName.QueryManagement.ElasticSearchs.Dtos
{
    public class PagingElasticSearchLogInput : PagingBase
    {
        public string Filter { get; set; }

        public DateTime? StartCreationTime { get; set; }

        public DateTime? EndCreationTime { get; set; }
    }
}