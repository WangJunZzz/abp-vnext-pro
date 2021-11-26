using System;
using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.ElasticsearchRepository.Dto
{
    public class PagingElasticSearchLogInput : PagingBase
    {
        public string Filter { get; set; }

        public DateTime? StartCreationTime { get; set; }

        public DateTime? EndCreationTime { get; set; }
    }
}