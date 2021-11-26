using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Users;

namespace CompanyName.ProjectName.Pages
{
    public class IndexModel : AbpPageModel
    {
        private readonly ICurrentUser _currentUser;

        public IndexModel(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
    }
}