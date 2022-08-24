namespace Lion.AbpPro.BasicManagement.OrganizationUnits.Dto;

public class UpdateOrganizationUnitInput
{
    [Required] 
    public string DisplayName { get; set; }

    public Guid Id { get; set; }
}