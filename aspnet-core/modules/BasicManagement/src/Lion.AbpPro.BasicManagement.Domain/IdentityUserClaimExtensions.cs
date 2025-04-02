namespace Lion.AbpPro.BasicManagement;

public static class IdentityUserClaimExtensions
{
    /// <summary>
    /// 获取用户首次修改密码时间
    /// </summary>
    public static DateTime? GetFirstChangePasswordTime(this IdentityUser user)
    {
        var claim = user.Claims.FirstOrDefault(e => e.ClaimType == BasicManagementConsts.FirstChangePasswordTime);
        if (claim == null) return null;
        DateTime.TryParse(claim.ClaimValue, out var result);
        return result;
    }
    
    /// <summary>
    /// 获取用户最后一次登录时间
    /// </summary>
    public static DateTime? GetLastChangePasswordTime(this IdentityUser user)
    {
        var claim = user.Claims.FirstOrDefault(e => e.ClaimType == BasicManagementConsts.LastChangePasswordTime);
        if (claim == null) return null;
        DateTime.TryParse(claim.ClaimValue, out var result);
        return result;
    }
}