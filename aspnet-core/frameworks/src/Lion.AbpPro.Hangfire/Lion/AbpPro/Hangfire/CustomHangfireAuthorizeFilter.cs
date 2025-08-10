namespace Lion.AbpPro.Hangfire;

public class CustomHangfireAuthorizeFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var currentUser = context.GetHttpContext().RequestServices.GetRequiredService<ICurrentUser>();
        return currentUser.IsAuthenticated;
    }
}