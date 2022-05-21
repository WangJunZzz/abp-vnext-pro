using System;
using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.OrganizationUnits.Dto;

public class GetOrganizationUnitRoleInput : PagingBase
{
    public Guid OrganizationUnitId { get; set; }

}