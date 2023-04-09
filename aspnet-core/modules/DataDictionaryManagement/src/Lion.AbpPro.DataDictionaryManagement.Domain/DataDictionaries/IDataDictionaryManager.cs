namespace Lion.AbpPro.DataDictionaryManagement.DataDictionaries;

public interface IDataDictionaryManager
{
    Task<DataDictionaryDto> FindByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<DataDictionaryDto> FindByCodeAsync(
        string code,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 创建字典类型
    /// </summary>
    /// <param name="code"></param>
    /// <param name="displayText"></param>
    /// <param name="description"></param>
    Task<DataDictionary> CreateAsync(string code, string displayText, string description);

    /// <summary>
    /// 新增字典明细
    /// </summary>
    /// <param name="dataDictionaryId"></param>
    /// <param name="code"></param>
    /// <param name="displayText"></param>
    /// <param name="description"></param>
    /// <param name="order"></param>
    /// <exception cref="DataDictionaryDomainException"></exception>
    Task<DataDictionary> CreateDetailAsync(
        Guid dataDictionaryId,
        string code,
        string displayText,
        string description,
        int order);

    /// <summary>
    /// 设置字典明细状态
    /// </summary>
    Task<DataDictionary> SetStatus(
        Guid dataDictionaryId,
        Guid dataDictionaryDetailId,
        bool isEnabled);

    /// <summary>
    /// 更新数据字典明细
    /// </summary>
    Task<DataDictionary> UpdateDetailAsync(
        Guid dataDictionaryId,
        Guid dataDictionaryDetailId,
        string displayText,
        string description,
        int order);

    Task DeleteAsync(Guid dataDictionaryId, Guid dataDictionaryDetailId);

    Task<DataDictionary> UpdateAsync(
        Guid dataDictionaryId,
        string displayText,
        string description);

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteDataDictionaryTypeAsync(Guid id);

    IAbpLazyServiceProvider LazyServiceProvider { get; set; }
    IServiceProvider ServiceProvider { get; set; }
}