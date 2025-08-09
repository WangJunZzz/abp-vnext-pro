namespace Lion.AbpPro.AspNetCore.Options;

public class AbpProCorsOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// 跨越设置，多个用英文逗号隔开。
    /// </summary>
    public string CorsOrigins { get; set; }
}