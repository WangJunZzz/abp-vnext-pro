using System.Net;

namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos
{
    public class GetAuditLogsInput
    {
        public string Sorting { get; set; }

        public int MaxResultCount { get; set; } = 50;

        public int SkipCount { get; set; } = 0;

        public DateTime? StartExecutionTime { get; set; }

        public DateTime? EndExecutionTime { get; set; }

        public string HttpMethod { get; set; }

        public string Url { get; set; }

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public string ApplicationName { get; set; }

        public string ClientIpAddress { get; set; }

        public string CorrelationId { get; set; }

        public int? MaxExecutionDuration { get; set; }

        public int? MinExecutionDuration { get; set; }

        public bool? HasException { get; set; }

        public HttpStatusCode? HttpStatusCode { get; set;}

        public bool IncludeDetails { get; set; } = true;
    }   
}
