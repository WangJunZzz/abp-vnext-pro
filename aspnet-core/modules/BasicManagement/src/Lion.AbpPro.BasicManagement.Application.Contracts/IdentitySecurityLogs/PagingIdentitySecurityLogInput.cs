namespace Lion.AbpPro.BasicManagement.IdentitySecurityLogs;

public class PagingIdentitySecurityLogInput: PagingBase
{
    /// <summary>
    /// 排序
    /// </summary>
    public string Sorting { get; set; }
    
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    public string Identity { get;  set; }
    
    /// <summary>
    /// 请求地址
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid? UserId { get; set; }
        
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 应用程序名称
    /// </summary>
    public string ApplicationName { get; set; }

    /// <summary>
    /// RequestId
    /// </summary>
    public string CorrelationId { get; set; }
    
    /// <summary>
    /// ClientId
    /// </summary>
    public string ClientId { get; set; }
}