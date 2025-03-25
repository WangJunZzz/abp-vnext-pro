namespace Lion.AbpPro.CAP;

public static class AbpProCAPMessageExtensions
{
    /// <summary>
    /// 尝试获取消息标头中的租户标识
    /// </summary>
    public static bool TryGetTenantId(this Message message, out Guid? tenantId)
    {
        if (message.Headers.TryGetValue(AbpProCapConsts.Tenant, out string tenantStr))
        {
            if (Guid.TryParse(tenantStr, out Guid id))
            {
                tenantId = id;
                return true;
            }
        }

        tenantId = null;
        return false;
    }

    /// <summary>
    /// 获取消息标头中的租户标识
    /// </summary>
    public static Guid? GetTenantIdOrNull(this Message message)
    {
        if (message.TryGetTenantId(out Guid? tenantId))
        {
            return tenantId;
        }

        return null;
    }
}