using CompanyNameProjectName.Pages.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyNameProjectName.Users.Dtos
{
    public class GetUserListInput: CustomeRequestDto
    {
        public string filter { get; set; }
    }
}
