namespace Lion.AbpPro.OrganizationUnits.Dto;

public class AddUserToOrganizationUnitInput
{
    public List<Guid> UserId { get; set; }
    
    public Guid OrganizationUnitId { get; set; }
}