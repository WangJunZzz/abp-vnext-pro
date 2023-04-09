using System.ComponentModel.DataAnnotations;

namespace Lion.AbpPro.LanguageManagement.Languages;

/// <summary>
/// 创建语言
/// </summary>
public class CreateLanguageInput
{
        /// <summary>
        /// 语言名称
        /// </summary>
        [Required(ErrorMessage = "语言名称不能为空")]
        public string CultureName { get;  set; }
        /// <summary>
        /// Ui语言名称
        /// </summary>
        [Required(ErrorMessage = "Ui语言名称不能为空")]
        public string UiCultureName { get;  set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [Required(ErrorMessage = "显示名称不能为空")]
        public string DisplayName { get;  set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string FlagIcon { get;  set; }            
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required(ErrorMessage = "是否启用不能为空")]
        public bool IsEnabled { get;  set; }
}