namespace Lion.AbpPro.BasicManagement.Roles.Dtos
{
    public class UpdateRolePermissionsInput : IValidatableObject
    {
        public string ProviderName { get; set; }

        public string ProviderKey { get; set; }

        public UpdatePermissionsDto UpdatePermissionsDto { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localization = validationContext.GetRequiredService<IStringLocalizer<AbpProLocalizationResource>>();
            if (ProviderName.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(ProviderName)],
                    new[] { nameof(ProviderName) }
                );
            }

            if (ProviderKey.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(ProviderKey)],
                    new[] { nameof(ProviderKey) }
                );
            }
        }
    }
}