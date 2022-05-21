using System;
using System.Collections.Generic;

namespace Lion.AbpPro.OrganizationUnits.Dto;

public class AddUserToOrganizationUnitInput
{
    public List<Guid> UserId { get; set; }
    
    public Guid OrganizationUnitId { get; set; }
}