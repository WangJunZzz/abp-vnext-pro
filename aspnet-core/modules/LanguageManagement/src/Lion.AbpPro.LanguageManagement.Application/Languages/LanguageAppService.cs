namespace Lion.AbpPro.LanguageManagement.Languages;

/// <summary>
/// 语言
/// </summary>
[Authorize(LanguageManagementPermissions.Languages.Default)]
public class LanguageAppService : ApplicationService, ILanguageAppService
{
    private readonly ILanguageManager _languageManager;
    private readonly ISettingManager _settingManager;

    public LanguageAppService(ILanguageManager languageManager, ISettingManager settingManager)
    {
        _languageManager = languageManager;
        _settingManager = settingManager;
    }

    /// <summary>
    /// 获取所有语言
    /// </summary>     
    public virtual async Task<List<PageLanguageOutput>> AllListAsync()
    {
        var languages = await _languageManager.ListAsync();
        var list = ObjectMapper.Map<List<Language>, List<PageLanguageOutput>>(languages);
        return list.ToList();
    }


    /// <summary>
    /// 分页查询语言
    /// </summary>     
    public virtual async Task<PagedResultDto<PageLanguageOutput>> PageAsync(PageLanguageInput input)
    {
        var result = new PagedResultDto<PageLanguageOutput>();
        var totalCount = await _languageManager.CountAsync(input.Filter);
        result.TotalCount = totalCount;
        if (totalCount <= 0) return result;
        var list = await _languageManager.GetListAsync(input.PageSize, input.SkipCount, input.Filter);
        result.Items = ObjectMapper.Map<List<LanguageDto>, List<PageLanguageOutput>>(list);
        return result;
    }

    /// <summary>
    /// 创建语言
    /// </summary>
    [Authorize(LanguageManagementPermissions.Languages.Create)]
    public virtual Task CreateAsync(CreateLanguageInput input)
    {
        return _languageManager.CreateAsync(
            GuidGenerator.Create(),
            input.CultureName,
            input.UiCultureName,
            input.DisplayName,
            input.FlagIcon,
            input.IsEnabled
        );
    }

    /// <summary>
    /// 编辑语言
    /// </summary>
    [Authorize(LanguageManagementPermissions.Languages.Update)]
    public virtual Task UpdateAsync(UpdateLanguageInput input)
    {
        return _languageManager.UpdateAsync(
            input.Id,
            input.CultureName,
            input.UiCultureName,
            input.DisplayName,
            input.FlagIcon,
            input.IsEnabled
        );
    }

    /// <summary>
    /// 删除语言
    /// </summary>
    [Authorize(LanguageManagementPermissions.Languages.Delete)]
    public virtual Task DeleteAsync(DeleteLanguageInput input)
    {
        return _languageManager.DeleteAsync(input.Id);
    }

    /// <summary>
    /// 设置默认语言
    /// </summary>
    [Authorize(LanguageManagementPermissions.Languages.ChangeDefault)]
    public virtual async Task SetDefaultAsync(IdInput input)
    {
        var language = await _languageManager.GetAsync(input.Id);
        await _settingManager.SetForCurrentTenantAsync(LanguageManagementConsts.SettingDefaultLanguage, language.CultureName + ";" + language.UiCultureName, false);
        await _languageManager.SetDefaultAsync(input.Id);
    }
}