using Lion.AbpPro.Localization;
using Volo.Abp.Localization;

namespace Lion.AbpPro.CAP;

public class LionAbpProCapPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var abpIdentityGroup = context.GetGroup(LionAbpProCapPermissions.CapManagement.Default);

        abpIdentityGroup.AddPermission(LionAbpProCapPermissions.CapManagement.Cap, L("Permission:Cap"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LionAbpProLocalizationResource>(name);
    }
}