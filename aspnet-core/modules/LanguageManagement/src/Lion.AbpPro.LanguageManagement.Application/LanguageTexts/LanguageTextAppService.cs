namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

/// <summary>
/// 语言文本
/// </summary>
[Authorize(LanguageManagementPermissions.LanguageTexts.Default)]
public class LanguageTextAppService : ApplicationService, ILanguageTextAppService
{
    private readonly ILanguageTextManager _languageTextManager;
    private readonly IStringLocalizerFactory _stringLocalizerFactory;
    private readonly ILocalizationHelper _localizationHelper;

    public LanguageTextAppService(
        ILanguageTextManager languageTextManager,
        IStringLocalizerFactory stringLocalizerFactory,
        IOptions<AbpLocalizationOptions> localizationOptions,
        IExternalLocalizationStore externalLocalizationStore,
        ILocalizationHelper localizationHelper)
    {
        _languageTextManager = languageTextManager;
        _stringLocalizerFactory = stringLocalizerFactory;
        _localizationHelper = localizationHelper;
    }

    /// <summary>
    /// 获取所有资源
    /// </summary>    
    public async Task<List<FromSelector<string, string>>> AllResourceListAsync()
    {
        var result = new List<FromSelector<string, string>>();
        foreach (var item in await _localizationHelper.GetAllResourceName())
        {
            result.Add(new FromSelector<string, string>(item, item));
        }

        return result;
    }

    /// <summary>
    /// 分页查询语言文本
    /// </summary>    
    public async Task<PagedResultDto<PageLanguageTextOutput>> PageAsync(PageLanguageTextInput input)
    {
        if (!CultureHelper.IsValidCultureCode(input.CultureName))
        {
            throw new LanguageManagementApplicationException(LanguageManagementErrorCodes.CultureNotValid);
        }

        var list = await FindLocalizationsAsync(input.CultureName);

        var queryable = list
            .AsQueryable()
            .WhereIf(input.ResourceName.IsNotNullOrWhiteSpace(), e => e.ResourceName == input.ResourceName)
            .WhereIf(input.Filter.IsNotNullOrWhiteSpace(), e => e.Name.Contains(input.Filter) || e.Value.Contains(input.Filter));
        var result = new PagedResultDto<PageLanguageTextOutput>
        {
            Items = queryable.OrderBy(e => e.Name).PageBy(input.SkipCount, input.PageSize).ToList(),
            TotalCount = queryable.Count()
        };

        return result;
    }

    protected virtual async Task<List<PageLanguageTextOutput>> FindLocalizationsAsync(string cultureName)
    {
        var list = new List<PageLanguageTextOutput>();
        using (CultureHelper.Use(cultureName))
        {
            foreach (var resource in await _localizationHelper.GetAllResourceName())
            {
                var localizer = await _stringLocalizerFactory.CreateByResourceNameOrNullAsync(resource);
                if (localizer != null)
                {
                    foreach (var localizedString in await localizer.GetAllStringsAsync(false, false, true))
                    {
                        var item = new PageLanguageTextOutput
                        {
                            //CultureName = resource.DefaultCultureName,
                            ResourceName = resource,
                            Name = localizedString.Name,
                            Value = localizedString.Value
                        };
                        if (list.Contains(item)) continue;
                        list.Add(item);
                    }
                }
            }
        }

        return list;
    }


    /// <summary>
    /// 创建语言文本
    /// </summary>
    [Authorize(LanguageManagementPermissions.LanguageTexts.Create)]
    public async Task CreateAsync(CreateLanguageTextInput input)
    {
        var localizedString = await GetLocalizedStringAsync(input.ResourceName, input.CultureName, input.Name);
        if (localizedString != null && localizedString.Value == input.Value)
        {
            throw new LanguageManagementApplicationException(LanguageManagementErrorCodes.LanguageTextExist).WithData("Name", input.Name);
        }

        await _languageTextManager.CreateAsync(
            GuidGenerator.Create(),
            input.CultureName,
            input.ResourceName,
            input.Name,
            input.Value
        );
    }

    /// <summary>
    /// 编辑语言文本
    /// </summary>
    [Authorize(LanguageManagementPermissions.LanguageTexts.Edit)]
    public async Task UpdateAsync(UpdateLanguageTextInput input)
    {
        var localizedString = await GetLocalizedStringAsync(input.ResourceName, input.CultureName, input.Name);
        if (localizedString == null) throw new LanguageManagementApplicationException(LanguageManagementErrorCodes.ResourceNotFound);
        if (localizedString.Value == input.Value) return;

        await _languageTextManager.UpdateAsync(
            input.CultureName,
            input.ResourceName,
            input.Name,
            input.Value
        );
    }

    /// <summary>
    /// 获取执行语言
    /// </summary>
    protected virtual async Task<LocalizedString> GetLocalizedStringAsync(string resourceName, string cultureName, string name)
    {
        using (CultureHelper.Use(cultureName))
        {
            if (!(await _localizationHelper.IsValidResourceName(resourceName)))

            {
                throw new LanguageManagementApplicationException(LanguageManagementErrorCodes.ResourceNotFound);
            }


            var localizer = await _stringLocalizerFactory.CreateByResourceNameOrNullAsync(resourceName);

            if (localizer == null)
            {
                throw new LanguageManagementApplicationException(LanguageManagementErrorCodes.ResourceNotFound);
            }

            var localizedStrings = await localizer.GetAllStringsAsync();

            var localizedString = localizedStrings.FirstOrDefault(e => e.Name == name);


            return localizedString;
        }
    }
}