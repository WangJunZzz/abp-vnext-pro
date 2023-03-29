namespace Lion.AbpPro.LanguageManagement.Languages;

/// <summary>
/// 创建语言
/// </summary>
public class PageLanguageOutput
{
       /// <summary>
       /// 语言Id
       /// </summary>
       public Guid Id { get; set; }

        /// <summary>
        /// 语言名称
        /// </summary>
        public string CultureName { get;  set; }
        /// <summary>
        /// Ui语言名称
        /// </summary>
        public string UiCultureName { get;  set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get;  set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string FlagIcon { get;  set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get;  set; }
       
        /// <summary>
        /// 创建时间
        /// </summary>       
        public DateTime CreationTime { get; set; }     
        
        /// <summary>
        /// 是否是默认语言
        /// </summary>
        public bool IsDefault { get; set; }
}