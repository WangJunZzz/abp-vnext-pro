namespace Lion.AbpPro.AuditLogs
{
    public class PagingAuditLogListInput : PagingBase
    {
        public string Sorting { get; set; } 
        public DateTime? StartTime { get; set; } 
        public DateTime? EndTime { get; set; } 
        public string HttpMethod { get; set; } 
        public string Url { get; set; } 
        public string UserName { get; set; } 
        public string ApplicationName { get; set; } 
        public string CorrelationId { get; set; } 
        public int? MaxExecutionDuration { get; set; } 
        public int? MinExecutionDuration { get; set; } 
        public bool? HasException { get; set; } 
        
        public HttpStatusCode? HttpStatusCode { get; set; } 
    }
}