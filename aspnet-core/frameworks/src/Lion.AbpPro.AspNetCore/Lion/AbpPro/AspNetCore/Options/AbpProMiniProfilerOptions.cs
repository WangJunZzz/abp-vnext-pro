namespace Lion.AbpPro.AspNetCore.Options;

public class AbpProMiniProfilerOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// path
    /// </summary>
    public string RouteBasePath { get; set; } = "/profiler";
}