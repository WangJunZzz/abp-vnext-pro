using System;
using System.ComponentModel.DataAnnotations;

namespace Lion.AbpPro.OrganizationUnits.Dto;

public class UpdateOrganizationUnitInput
{
    [Required] public string DisplayName { get; set; }

    public Guid Id { get; set; }
}