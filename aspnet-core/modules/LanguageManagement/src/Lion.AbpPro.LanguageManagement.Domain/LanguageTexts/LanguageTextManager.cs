namespace Lion.AbpPro.LanguageManagement.LanguageTexts;

public class LanguageTextManager : LanguageManagementDomainService, ILanguageTextManager
{
    private readonly ILanguageTextRepository _languageTextRepository;
    private readonly IObjectMapper _objectMapper;

    public LanguageTextManager(ILanguageTextRepository languageTextRepository, IObjectMapper objectMapper)
    {
        _languageTextRepository = languageTextRepository;
        _objectMapper = objectMapper;
    }

    /// <summary>
    /// 查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    /// <param name="maxResultCount">返回最大条数</param>
    /// <param name="skipCount">跳过条数</param>
    public async Task<List<LanguageTextDto>> ListAsync(string cultureName, string resourceName, string filter = null, int maxResultCount = 10, int skipCount = 0)
    {
        var list = await _languageTextRepository.ListAsync(cultureName, resourceName, filter, maxResultCount, skipCount);
        return ObjectMapper.Map<List<LanguageText>, List<LanguageTextDto>>(list);
    }

    /// <summary>
    /// 查询语言文本数量
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="filter">筛选条件：name or value</param>
    public async Task<long> CountAsync(string cultureName, string resourceName, string filter = null)
    {
        return await _languageTextRepository.CountAsync(cultureName, resourceName, filter);
    }

    /// <summary>
    /// 创建语言文本
    /// </summary>
    public async Task<LanguageTextDto> CreateAsync(Guid id, string cultureName, string resourceName, string name, string value)
    {
        var entity = await _languageTextRepository.FindAsync(cultureName, resourceName, name);
        if (entity != null)
        {
            throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageTextExist).WithData("Name", name);
        }

        entity = new LanguageText(id, cultureName, resourceName, name, value, CurrentTenant.Id);
        entity = await _languageTextRepository.InsertAsync(entity);
        return _objectMapper.Map<LanguageText, LanguageTextDto>(entity);
    }


    /// <summary>
    /// 更新语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    /// <param name="name">键</param> 
    /// <param name="value">值</param> 
    public async Task<LanguageTextDto> UpdateAsync(string cultureName, string resourceName, string name, string value)
    {
        var entity = await _languageTextRepository.FindAsync(cultureName, resourceName, name);
        if (entity == null)
        {
            entity = new LanguageText(GuidGenerator.Create(), cultureName, resourceName, name, value, CurrentTenant.Id);
            await _languageTextRepository.InsertAsync(entity);
        }
        else
        {
            entity.Update(cultureName, resourceName, name, value);
            entity = await _languageTextRepository.UpdateAsync(entity);
        }
        
        return _objectMapper.Map<LanguageText, LanguageTextDto>(entity);
    }

    /// <summary>
    /// 删除语言文本
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _languageTextRepository.FindAsync(id);
        if (entity == null) throw new LanguageManagementDomainException(LanguageManagementErrorCodes.LanguageNotFound);
        await _languageTextRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 根据资源名称和语言名称查询语言文本
    /// </summary>
    /// <param name="cultureName">语言</param>
    /// <param name="resourceName">资源名称</param>
    public virtual async Task<List<LanguageTextDto>> FindAsync(string cultureName, string resourceName)
    {
        var languageTexts = await _languageTextRepository.FindAsync(cultureName, resourceName);
        return _objectMapper.Map<List<LanguageText>, List<LanguageTextDto>>(languageTexts);
    }
}