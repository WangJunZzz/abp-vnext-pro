namespace Lion.AbpPro.AspNetCore.Options;

public class AbpProJwtOptions
{
    /// <summary>
    /// JWT令牌颁发者(Issuer)
    /// 用于标识令牌的颁发机构
    /// </summary>
    public string Issuer { get; set; }
    
    /// <summary>
    /// JWT令牌受众(Audience)
    /// 指定令牌的目标接收方
    /// </summary>
    public string Audience { get; set; }
    
    /// <summary>
    /// JWT安全密钥
    /// 用于签名和验证JWT令牌的密钥
    /// </summary>
    public string SecurityKey { get; set; }
    
    /// <summary>
    /// JWT令牌过期时间(分钟)
    /// 令牌在颁发后多少分钟过期
    /// </summary>
    public int ExpirationTime { get; set; }
}