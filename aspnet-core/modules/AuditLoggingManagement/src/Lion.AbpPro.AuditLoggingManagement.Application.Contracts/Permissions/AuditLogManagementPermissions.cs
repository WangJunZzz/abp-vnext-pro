using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lion.AbpPro.AuditLoggingManagement.Permissions
{
    public class AuditLogManagementPermissions
    {
        public const string GroupName = "AuditLogManagement";

        public static class AuditLogs
        {
            public const string Default = GroupName + ".AuditLogs";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }
    }
}
