using Lion.AbpPro.AuditLoggingManagement.Domain.Shared.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lion.AbpPro.AuditLoggingManagement.Permissions
{
    public class AuditLogManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public const string GroupName = "AuditLogManagement";

        public override void Define(IPermissionDefinitionContext context)
        {
            var auditLogsGroup = context.AddGroup(GroupName, L("AuditLogManagement"));

            var auditLogs = auditLogsGroup.AddPermission(AuditLogManagementPermissions.AuditLogs.Default, L("Permission:AuditLogManagement.AuditLogs"));
            auditLogs.AddChild(AuditLogManagementPermissions.AuditLogs.Delete, L("Permission:AuditLogManagement.AuditLogs.Delete"));      
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AuditLogManagementResource>(name);
        }
    }
}
