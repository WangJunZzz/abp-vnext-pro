namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

/// <summary>
/// 创建语言文本
/// </summary>
public class CreateLanguageTextInput : IValidatableObject
{
    /// <summary>
    /// 资源名称
    /// </summary>
    public string ResourceName { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string CultureName { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

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

        if (ResourceName.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(ResourceName)],
                new[] { nameof(ResourceName) }
            );
        }

        if (Name.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(Name)],
                new[] { nameof(Name) }
            );
        }

        if (Value.IsNullOrWhiteSpace())
        {
            yield return new ValidationResult(
                localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(Value)],
                new[] { nameof(Value) }
            );
        }
    }
}