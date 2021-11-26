using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Users;

namespace Lion.AbpPro.Pages
{
    [Authorize]
    public class Welcome : AbpPageModel
    {
        private readonly ICurrentUser _currentUser;

        public Welcome(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public void OnGet()
        {
        }
    }
}