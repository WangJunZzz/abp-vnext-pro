namespace Lion.AbpPro.BasicManagement.ConfigurationOptions
{
    public class JwtOptions
    {
        /// <summary>
        /// 过期时间 单位小时
        /// </summary>
        public int ExpirationTime { get; set; }

        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }
    }
}