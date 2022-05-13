using System;

namespace Lion.AbpPro.OrganizationUnits.Dto;

public class RemoveRoleToOrganizationUnitInput
{
    public Guid RoleId { get; set; }
    
    public Guid OrganizationUnitId { get; set; }
}