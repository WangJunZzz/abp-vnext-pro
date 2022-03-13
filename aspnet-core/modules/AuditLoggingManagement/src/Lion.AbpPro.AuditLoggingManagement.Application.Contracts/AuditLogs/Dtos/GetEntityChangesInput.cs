using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace Lion.AbpPro.AuditLoggingManagement.AuditLogs.Dtos
{
    public class GetEntityChangesInput
    {
        public string Sorting { set; get; }

        public int MaxResultCount { set; get; } = 50;

        public int SkipCount { set; get; } = 0;

        public Guid? AuditLogId { set; get; }

        public DateTime? StartTime { set; get; }

        public DateTime? SndTime { set; get; }

        public EntityChangeType? ChangeType { set; get; }

        public string EntityId { set; get; }

        public string EntityTypeFullName { set; get; }

        public bool IncludeDetails { set; get; } = true;
    }
}
