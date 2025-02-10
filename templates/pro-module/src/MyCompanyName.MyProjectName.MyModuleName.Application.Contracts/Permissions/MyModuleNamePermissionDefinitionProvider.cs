namespace MyCompanyName.MyProjectName.MyModuleName.Permissions
{
    public class MyModuleNamePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
           
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MyModuleNameResource>(name);
        }
    }
}