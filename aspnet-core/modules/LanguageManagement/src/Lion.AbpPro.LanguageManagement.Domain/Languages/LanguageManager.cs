namespace Lion.AbpPro.LanguageManagement.Languages;

public class LanguageManager : LanguageManagementDomainService, ILanguageManager
{
    private readonly ILanguageRepository _languageRepository;

    public LanguageManager(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    /// <summary>
    /// 查询语言
    /// </summary>
    public virtual async Task<List<Language>> ListAsync(bool? isEnabled = null)
    {
        var list = await _languageRepository.ListAsync(isEnabled);
        return list;
    }

    /// <summary>
    /// 查询语言
    /// </summary>
    public virtual async Task<List<LanguageDto>> GetListAsync(int maxResultCount = 10, int skipCount = 0, string filter = null)
    {
        var list = await _languageRepository.ListAsync(maxResultCount, skipCount, filter);
        return ObjectMapper.Map<List<Language>, List<LanguageDto>>(list);
    }

    /// <summary>
    /// 获取总条数
    /// </summary>
    /// <param name="filter">查询条件 cultureName or uiCultureName or displayName</param>
    public virtual async Task<long> CountAsync(string filter = null)
    {
        return await _languageRepository.CountAsync(filter);
    }

    /// <summary>
    /// 创建语言
    /// </summary>
    public virtual async Task<LanguageDto> CreateAsync(
        Guid id,
        string cultureName,
        string uiCultureName,
        string displayName,
        string flagIcon,
        bool isEnabled
    )
    {
        var entity = await _languageRepository.FindAsync(cultureName);
        if (entity != null)
        {
            throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageExist);
        }

        entity = new Language(id, cultureName, uiCultureName, displayName, flagIcon, isEnabled, false);
        entity = await _languageRepository.InsertAsync(entity);
        return ObjectMapper.Map<Language, LanguageDto>(entity);
    }

    /// <summary>
    /// 更新语言
    /// </summary>
    public virtual async Task<LanguageDto> UpdateAsync(
        Guid id,
        string cultureName,
        string uiCultureName,
        string displayName,
        string flagIcon,
        bool isEnabled
    )
    {
        var entity = await _languageRepository.FindAsync(id);
        if (entity == null) throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageNotFound);
        entity.Update(cultureName, uiCultureName, displayName, flagIcon, isEnabled);
        entity = await _languageRepository.UpdateAsync(entity);
        return ObjectMapper.Map<Language, LanguageDto>(entity);
    }

    /// <summary>
    /// 删除语言
    /// </summary>
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _languageRepository.FindAsync(id);
        if (entity == null) throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageNotFound);
        await _languageRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 通过Id获取语言
    /// </summary>
    public virtual async Task<LanguageDto> GetAsync(Guid id)
    {
        var entity = await _languageRepository.FindAsync(id);
        if (entity == null) throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageNotFound);
        return ObjectMapper.Map<Language, LanguageDto>(entity);
    }

    /// <summary>
    /// 获取指定语言
    /// </summary>
    public virtual async Task<LanguageDto> GetAsync(string cultureName)
    {
        var entity = await _languageRepository.FindAsync(cultureName);
        if (entity == null) throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageNotFound);
        return ObjectMapper.Map<Language, LanguageDto>(entity);
    }

    /// <summary>
    /// 设置默认语言
    /// </summary>
    public virtual async Task SetDefaultAsync(Guid id)
    {
        var entity = await _languageRepository.FindAsync(id);
        if (entity == null) throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageNotFound);

        var defaultLanguage = await _languageRepository.FindDefaultLanguageAsync();

        if (defaultLanguage != null && entity.Id == defaultLanguage.Id) return;

        defaultLanguage.SetDefault(false);
        entity.SetDefault(true);

        await _languageRepository.UpdateAsync(entity);
        await _languageRepository.UpdateAsync(defaultLanguage);
    }
}