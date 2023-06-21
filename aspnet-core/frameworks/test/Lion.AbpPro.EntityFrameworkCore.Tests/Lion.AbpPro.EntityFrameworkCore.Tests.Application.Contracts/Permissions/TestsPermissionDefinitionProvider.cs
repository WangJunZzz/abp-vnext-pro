using Lion.AbpPro.EntityFrameworkCore.Tests.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Permissions;

public class TestsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestsPermissions.GroupName, L("Permission:Tests"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestsResource>(name);
    }
}
