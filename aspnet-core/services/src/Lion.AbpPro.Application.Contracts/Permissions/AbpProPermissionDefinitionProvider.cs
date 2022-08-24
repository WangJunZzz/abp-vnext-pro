namespace Lion.AbpPro.Permissions
{
    public class AbpProPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var abpIdentityGroup = context.GetGroup(IdentityPermissions.GroupName);
    
            var esManagement = abpIdentityGroup.AddPermission(AbpProPermissions.SystemManagement.ES, L("Permission:ESManagement"));
       

       
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProResource>(name);
        }
    }
}