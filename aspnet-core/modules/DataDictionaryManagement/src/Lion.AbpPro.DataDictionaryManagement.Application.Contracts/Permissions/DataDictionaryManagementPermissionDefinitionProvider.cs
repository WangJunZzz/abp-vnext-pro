using Lion.AbpPro.DataDictionaryManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lion.AbpPro.DataDictionaryManagement.Permissions
{
    public class DataDictionaryManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(DataDictionaryManagementPermissions.GroupName, L("Permission:DataDictionaryManagement"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DataDictionaryManagementResource>(name);
        }
    }
}