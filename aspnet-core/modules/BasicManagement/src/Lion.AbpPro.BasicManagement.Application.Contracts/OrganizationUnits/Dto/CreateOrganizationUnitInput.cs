namespace Lion.AbpPro.BasicManagement.OrganizationUnits.Dto;

public class CreateOrganizationUnitInput
{
    [Required] 
    public string DisplayName { get; set; }

    public Guid? ParentId { get; set; }
}