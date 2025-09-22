namespace Lion.AbpPro.AspNetCore.Options;

public class AbpProConsulOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } 
    
    /// <summary>
    /// consul服务地址
    /// </summary>
    public string ServiceUrl { get; set; }
    
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ClientName { get; set; }
    
    /// <summary>
    /// 服务地址，不带端口
    /// </summary>
    public string ClientAddress { get; set; }
    
    /// <summary>
    /// 服务端口
    /// </summary>
    public int ClientPort { get; set; }
    
    /// <summary>
    /// 健康检查地址
    /// </summary>
    public string HealthUrl { get; set; }
    
    /// <summary>
    /// 服务停止多久后注销,单位秒
    /// </summary>
    public int DeregisterCriticalServiceAfter { get; set; }
    
    /// <summary>
    /// 健康检查时间间隔，或者称为心跳 间隔,单位秒
    /// </summary>
    public int Interval { get; set; }
    
    /// <summary>
    /// 超时时间,单位秒
    /// </summary>
    public int Timeout { get; set; }
}