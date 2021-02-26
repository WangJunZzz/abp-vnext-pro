using Zzz.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Zzz.Permissions
{
    public class ZzzPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {


            //Define your own permissions here. Example:
            //myGroup.AddPermission(ZzzPermissions.MyPermission1, L("Permission:MyPermission1"));

            #region 数据字典
            var dicGroupName = context.AddGroup(ZzzPermissions.DicGroupName,L("Permission:Dic"));
            var dicPermission = dicGroupName.AddPermission(ZzzPermissions.Dic.Default, L("Permission:DicManager"));
            dicPermission.AddChild(ZzzPermissions.Dic.Query, L("Permission:Query"));
            dicPermission.AddChild(ZzzPermissions.Dic.Create, L("Permission:Create"));
            dicPermission.AddChild(ZzzPermissions.Dic.Update, L("Permission:Update"));
            dicPermission.AddChild(ZzzPermissions.Dic.Delete, L("Permission:Delete"));
            #endregion

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ZzzResource>(name);
        }
    }
}
