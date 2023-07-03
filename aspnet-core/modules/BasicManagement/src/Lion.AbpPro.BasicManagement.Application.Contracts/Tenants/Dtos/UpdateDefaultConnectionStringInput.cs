namespace Lion.AbpPro.BasicManagement.Tenants.Dtos
{
    public class UpdateConnectionStringInput : IValidatableObject
    {
        public Guid Id { get; set; }
        public string ConnectionString { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localization = validationContext.GetRequiredService<IStringLocalizer<AbpProLocalizationResource>>();
            if (ConnectionString.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    localization[AbpProLocalizationErrorCodes.ErrorCode100003, nameof(ConnectionString)],
                    new[] { nameof(ConnectionString) }
                );
            }
        }
    }
}