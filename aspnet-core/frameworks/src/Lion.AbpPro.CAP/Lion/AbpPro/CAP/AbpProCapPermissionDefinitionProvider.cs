namespace Lion.AbpPro.CAP;

public class AbpProCapPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var abpIdentityGroup = context.GetGroup(AbpProCapPermissions.CapManagement.Default);

        abpIdentityGroup.AddPermission(AbpProCapPermissions.CapManagement.Cap, L("Permission:Cap"), multiTenancySide: MultiTenancySides.Both);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpProLocalizationResource>(name);
    }
}