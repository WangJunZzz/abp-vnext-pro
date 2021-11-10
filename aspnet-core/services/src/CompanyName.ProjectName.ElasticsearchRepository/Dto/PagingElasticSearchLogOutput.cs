using System;
using Nest;

namespace CompanyName.ProjectName.ElasticsearchRepository.Dto
{
    [Serializable]
    public class PagingElasticSearchLogOutput
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }


        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [PropertyName("@timestamp")]
        public DateTime CreationTime { get; set; }
    }
}