using System;
using System.ComponentModel.DataAnnotations;

namespace Lion.AbpPro.OrganizationUnits.Dto;

public class CreateOrganizationUnitInput
{
    [Required] public string DisplayName { get; set; }

    public Guid? ParentId { get; set; }
}