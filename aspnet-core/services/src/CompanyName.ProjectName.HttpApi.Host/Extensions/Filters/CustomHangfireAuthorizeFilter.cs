using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Users;

namespace CompanyNameProjectName.Extensions.Filters
{
    public class CustomHangfireAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var _currentUser = context.GetHttpContext().RequestServices.GetRequiredService<ICurrentUser>();
            return _currentUser.IsAuthenticated;
        }
    }
}
