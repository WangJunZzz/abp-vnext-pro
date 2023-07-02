namespace Lion.AbpPro.Permissions
{
    public class AbpProPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpProResource>(name);
        }
    }
}