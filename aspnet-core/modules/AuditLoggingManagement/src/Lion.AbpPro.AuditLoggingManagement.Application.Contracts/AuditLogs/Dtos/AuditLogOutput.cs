namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos
{
    public class AuditLogOutput
    {
        public Guid Id { set; get; }

        public string ApplicationName { get; set; }

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public Guid? TenantId { get; set; }

        public string TenantName { get; set; }

        public DateTime ExecutionTime { get; set; }

        public int ExecutionDuration { get; set; }

        public string ClientIpAddress { get; set; }

        public string ClientName { get; set; }

        public string CorrelationId { get; set; }

        public string HttpMethod { get; set; }

        public string Url { get; set; }

        public string Exceptions { get; set; }

        public int? HttpStatusCode { get; set; }

        //public ICollection<EntityChangeOutput> EntityChanges { get; set; }

        public ICollection<AuditLogActionOutput> Actions { get; set; }
    }
}
