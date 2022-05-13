using System;
using Lion.AbpPro.Extension.Customs.Dtos;

namespace Lion.AbpPro.OrganizationUnits.Dto;

public class GetOrganizationUnitUserInput : PagingBase
{
    public Guid OrganizationUnitId { get; set; }

    public string Filter { get; set; }
}