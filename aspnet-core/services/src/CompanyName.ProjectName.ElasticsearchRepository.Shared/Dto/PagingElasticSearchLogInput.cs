using System;
using CompanyName.ProjectName.Extension.Customs.Dtos;

namespace CompanyName.ProjectName.ElasticsearchRepository.Dto
{
    public class PagingElasticSearchLogInput : PagingBase
    {
        public string Filter { get; set; }

        public DateTime? StartCreationTime { get; set; }

        public DateTime? EndCreationTime { get; set; }
    }
}