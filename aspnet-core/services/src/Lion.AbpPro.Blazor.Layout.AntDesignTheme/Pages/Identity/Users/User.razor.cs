using System.Threading.Tasks;
using Lion.AbpPro.Users;
using Lion.AbpPro.Users.Dtos;
using Microsoft.AspNetCore.Components;

namespace Lion.AbpPro.Blazor.Layout.AntDesignTheme.Pages.Identity.Users;

public partial class User
{
    [Inject] protected  IAccountAppService _accountAppService { get; set; }



    protected override async Task OnInitializedAsync()
    {
        var res = await _accountAppService.LoginAsync(new LoginInput
            { Name = "admin", Password = "1q2w3E*" });
        base.OnInitializedAsync();
    }
}