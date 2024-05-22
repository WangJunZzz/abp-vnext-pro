namespace Lion.AbpPro.BasicManagement.Tenants.Dtos;

public class DeleteConnectionStringInput
{
    /// <summary>
    /// 连接字符串名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 租户id    
    /// </summary>
    public Guid TenantId { get; set; }
}