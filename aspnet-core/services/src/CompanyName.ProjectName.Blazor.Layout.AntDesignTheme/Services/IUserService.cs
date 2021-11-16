using CompanyName.ProjectName.Blazor.Layout.AntDesignTheme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Blazor.Layout.AntDesignTheme.Services
{
    public interface IUserService
    {
        Task<CurrentUser> GetCurrentUserAsync();
    }
}
