namespace Lion.AbpPro.LanguageManagement.Languages;

/// <summary>
/// 删除语言
/// </summary>
public class UpdateLanguageInput : IValidatableObject
{
    /// <summary>
    /// 语言Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string CultureName { get; set; }

    /// <summary>
    /// Ui语言名称
    /// </summary>
    public string UiCultureName { get; set; }

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string FlagIcon { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var localization = validationContext.GetRequiredService<IStringLocalizer<AbpProLocalizationResource>>();
        if (CultureName.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(CultureName)],
                new[] { nameof(CultureName) }
            );
        }

        if (UiCultureName.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(UiCultureName)],
                new[] { nameof(UiCultureName) }
            );
        }

        if (DisplayName.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(DisplayName)],
                new[] { nameof(DisplayName) }
            );
        }
    }
}