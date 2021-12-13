using AntDesign;
using AntDesign.ProLayout;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Lion.AbpPro.Blazor.Layout.AntDesignTheme.Components.GlobalHeader
{
    public partial class RightContent : ITransientDependency
    {
        // private readonly HttpClient _httpClient;
        // private readonly ILocalStorageService _localStorage;
        // private readonly MessageService _messageService;
        // private readonly NavigationManager _navigationManager;
        // private IDictionary<string, string> _languageLabels = new Dictionary<string, string>();
        // private string[] _locales;
        // private IDictionary<string, string> _languageIcons = new Dictionary<string, string>();
        // public RightContent(
        //     HttpClient httpClient,
        //     ILocalStorageService localStorage,
        //     MessageService messageService,
        //     NavigationManager navigationManager
        //     )
        // {
        //     _httpClient = httpClient;
        //     _localStorage = localStorage;
        //     _messageService = messageService;
        //     _navigationManager = navigationManager;
        // }
        //
        // public string FullScreenTipMsg { get; set; } = "全屏";
        //
        // public AvatarMenuItem[] AvatarMenuItems { get; } = new AvatarMenuItem[]
        // {
        //     new() { Key = "setting", IconType = "setting", Option = "个人设置"},
        //     new() { Key = "center", IconType = "user", Option = "锁定屏幕"},
        //     new() { IsDivider = true },
        //     new() { Key = "logout", IconType = "logout", Option = "注销"}
        // };
        // protected override async Task OnInitializedAsync()
        // {
        //     await base.OnInitializedAsync();
        //
        //     ClearClassMap();
        //
        //     _languageLabels.Add("zh-Hans", "简体中文");
        //     _languageLabels.Add("en", "English");
        //
        //     _languageIcons.Add("zh-Hans", "\ud83c\udde8\ud83c\uddf3");
        //     _languageIcons.Add("en", "\ud83c\uddfa\ud83c\uddf8");
        //
        //     _locales = new string[] { "zh-Hans", "en" };
        // }
        //
        // private void ClearClassMap()
        // {
        //     ClassMapper.Clear().Add("right");
        // }
        //
        // public async Task HandleClear(string key)
        // {
        //     switch (key)
        //     {
        //         case "notification":
        //             // TODO
        //             break;
        //         case "message":
        //             // TODO
        //             break;
        //     }
        //
        //     await _messageService.Success($"清空了{key}");
        // }
        //
        // public async Task HandleViewMore(string key)
        // {
        //     await _messageService.Info("查看更多...");
        // }
        //
        // public async Task HandleSelectUser(MenuItem item)
        // {
        //     switch (item.Key)
        //     {
        //         case "center":
        //             _navigationManager.NavigateTo("/account/center");
        //             break;
        //         case "setting":
        //             _navigationManager.NavigateTo("/account/settings");
        //             break;
        //         case "logout":
        //
        //             //  先调用服务端登出接口  TODO
        //
        //             //  再清楚本地应用token缓存
        //             await _localStorage.RemoveItemAsync("access_token");
        //             _httpClient.DefaultRequestHeaders.Authorization = null;
        //
        //             _navigationManager.NavigateTo("/user/login", true);
        //             break;
        //         default:
        //             break;
        //     }
        //     await Task.CompletedTask;
        // }
        //
        // /// <summary>
        // /// 多语言
        // /// </summary>
        // /// <param name="item"></param>
        // public async Task HandleSelectLang(MenuItem item)
        // {
        //     var langText = item.Key switch
        //     {
        //         "zh-Hans" => "简体中文",
        //         _ => "English",
        //     };
        //     string lang = item.Key;
        //     await _localStorage.SetItemAsStringAsync("lang", lang);
        //
        //     //await _accountAppService.ChangeLanguageAsync(lang);  //TODO
        //
        //     CultureInfo culture = new(lang);
        //     CultureInfo.DefaultThreadCurrentCulture = culture;
        //     CultureInfo.DefaultThreadCurrentUICulture = culture;
        //
        //     await _messageService.Success($"切换【{langText}】成功!");
        //
        //     var currentUrl = _navigationManager.Uri;
        //     _navigationManager.NavigateTo(currentUrl, true);
        // }
    }
}
