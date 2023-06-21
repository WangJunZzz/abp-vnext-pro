using Volo.Abp.Reflection;

namespace Lion.AbpPro.EntityFrameworkCore.Tests.Permissions;

public class TestsPermissions
{
    public const string GroupName = "Tests";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TestsPermissions));
    }
}
