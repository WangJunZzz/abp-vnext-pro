namespace Lion.AbpPro.BasicManagement.Roles;

public class PermissionOptions
{
    /// <summary>
    /// 需要排除的权限
    /// </summary>
    public List<string> Excludes { get; }

    public PermissionOptions()
    {
        Excludes = new List<string>();
    }

    /// <summary>
    /// 权限是否排除
    /// </summary>
    /// <param name="permission">权限名称</param>
    /// <returns>bool</returns>
    public bool IsExclude(string permission)
    {
        if (permission.IsNullOrWhiteSpace()) return false;
        return Excludes.Any(e => e.ToLower() == permission.ToLower());
    }
}