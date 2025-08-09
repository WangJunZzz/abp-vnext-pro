namespace Lion.AbpPro.AspNetCore.Options;

public class AbpProGatewayOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } 
    
    /// <summary>
    /// consul服务地址
    /// </summary>
    public string ConsulServiceUrl { get; set; }
    
    /// <summary>
    /// consul服务名称
    /// </summary>
    public string ServiceName { get; set; }
    
    /// <summary>
    /// 健康检查地址
    /// </summary>
    public string HealthUrl { get; set; }
}