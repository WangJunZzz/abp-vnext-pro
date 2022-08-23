using System.Net;

namespace Lion.AbpPro.BasicManagement.AuditLogs
{
    public class PagingAuditLogListInput : PagingBase
    {
        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// RequestId
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// 最大执行时间
        /// </summary>
        public int? MaxExecutionDuration { get; set; }

        /// <summary>
        /// 最小执行时间
        /// </summary>
        public int? MinExecutionDuration { get; set; }

        /// <summary>
        /// 是否有异常
        /// </summary>
        public bool? HasException { get; set; }

        /// <summary>
        /// http 状态码
        /// </summary>
        public HttpStatusCode? HttpStatusCode { get; set; }
    }
}