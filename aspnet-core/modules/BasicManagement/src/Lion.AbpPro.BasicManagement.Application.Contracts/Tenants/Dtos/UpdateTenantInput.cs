namespace Lion.AbpPro.BasicManagement.Tenants.Dtos
{
    public class UpdateTenantInput : IValidatableObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

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
        }
    }
}