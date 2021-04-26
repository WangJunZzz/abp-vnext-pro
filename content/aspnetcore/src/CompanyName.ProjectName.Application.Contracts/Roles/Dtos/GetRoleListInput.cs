using CompanyNameProjectName.Pages.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyNameProjectName.Roles.Dtos
{
    public class GetRoleListInput : CustomeRequestDto
    {
        public string filter { get; set; }
    }
}
