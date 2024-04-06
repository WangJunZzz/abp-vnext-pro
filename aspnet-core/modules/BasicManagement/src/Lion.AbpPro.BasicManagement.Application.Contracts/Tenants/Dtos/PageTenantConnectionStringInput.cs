namespace Lion.AbpPro.BasicManagement.Tenants.Dtos;

public class PageTenantConnectionStringInput
{
    /// <summary>
    /// 租户id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 连接字符串名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 连接字符串地址
    /// </summary>
    public string Value { get; set; }
}