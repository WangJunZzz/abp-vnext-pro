namespace Lion.AbpPro.BasicManagement.Tenants.Dtos;

public class AddOrUpdateConnectionStringInput : IValidatableObject
{
    
    /// <summary>
    /// id
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 连接字符串名称
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// 连接字符串地址
    /// </summary>
    public string Value { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var localization = validationContext.GetRequiredService<IStringLocalizer<AbpProLocalizationResource>>();
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