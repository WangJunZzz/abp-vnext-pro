using System.Threading.Tasks;
using Lion.AbpPro.Users;
using Lion.AbpPro.Users.Dtos;

namespace Lion.AbpPro.Blazor.Layout.AntDesignTheme.Pages.Identity.Users;

public partial class User
{
    private readonly IAccountAppService _accountAppService;

    public User(IAccountAppService accountAppService)
    {
        _accountAppService = accountAppService;
    }

    protected override async Task OnInitializedAsync()
    {
        var res = await _accountAppService.LoginAsync(new LoginInput
            { Name = "admin", Password = "1q2w3E*" });
        base.OnInitializedAsync();
    }
}