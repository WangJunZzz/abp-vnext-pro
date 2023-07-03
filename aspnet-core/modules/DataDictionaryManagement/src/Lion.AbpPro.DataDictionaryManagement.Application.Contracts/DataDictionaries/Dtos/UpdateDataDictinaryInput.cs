namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Dtos
{
    public class UpdateDataDictinaryInput : IValidatableObject
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string DisplayText { get; set; }
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localization = validationContext.GetRequiredService<IStringLocalizer<AbpProLocalizationResource>>();
            if (Code.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(Code)],
                    new[] { nameof(Code) }
                );
            }

            if (DisplayText.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(DisplayText)],
                    new[] { nameof(DisplayText) }
                );
            }
        }
    }
}