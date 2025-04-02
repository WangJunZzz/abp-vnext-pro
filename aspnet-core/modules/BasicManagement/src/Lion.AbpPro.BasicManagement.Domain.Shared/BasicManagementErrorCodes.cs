namespace Lion.AbpPro.BasicManagement;

public static class BasicManagementErrorCodes
{
    public const string OrganizationUnitNotExist = BasicManagementConsts.NameSpace + ":100001";
    public const string UserLockedOut = BasicManagementConsts.NameSpace + ":100002";
    public const string UserOrPasswordMismatch = BasicManagementConsts.NameSpace + ":100003";
    public const string UserDisabled = BasicManagementConsts.NameSpace + ":100004";
    public const string TenantNotExist = BasicManagementConsts.NameSpace + ":100005";
    public const string NotSupportSetConnectionString = BasicManagementConsts.NameSpace + ":100006";
    public const string UserNotExist = BasicManagementConsts.NameSpace + ":100007";
    public const string PasswordExpire = BasicManagementConsts.NameSpace + ":100008";
    public const string NewPasswordExpire = BasicManagementConsts.NameSpace + ":100009";
    public const string OldPasswordExpire = BasicManagementConsts.NameSpace + ":100010";
}