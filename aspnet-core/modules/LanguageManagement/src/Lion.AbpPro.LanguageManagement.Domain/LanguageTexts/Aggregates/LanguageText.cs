namespace Lion.AbpPro.LanguageManagement.LanguageTexts.Aggregates;

public class LanguageText : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    private LanguageText()
    {
    }


    public LanguageText(
        Guid id,
        string cultureName,
        string resourceName,
        string name,
        string value,
        Guid? tenantId = null
    ) : base(id)
    {
        SetResourceName(resourceName);
        SetCultureName(cultureName);
        SetName(name);
        SetValue(value);
        TenantId = tenantId;
    }

    public Guid? TenantId { get; private set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string CultureName { get; private set; }


    /// <summary>
    /// 资源名称
    /// </summary>
    public string ResourceName { get; private set; }


    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; private set; }


    /// <summary>
    /// 设置资源名称
    /// </summary>        
    private void SetResourceName(string resourceName)
    {
        Guard.NotNullOrWhiteSpace(resourceName, nameof(resourceName), 128);
        ResourceName = resourceName;
    }

    /// <summary>
    /// 设置语言名称
    /// </summary>        
    private void SetCultureName(string cultureName)
    {
        Guard.NotNullOrWhiteSpace(cultureName, nameof(cultureName), 128);
        CultureName = cultureName;
    }

    /// <summary>
    /// 设置名称
    /// </summary>        
    private void SetName(string name)
    {
        Guard.NotNullOrWhiteSpace(name, nameof(name), 256);
        Name = name;
    }

    /// <summary>
    /// 设置值
    /// </summary>        
    private void SetValue(string value)
    {
        Guard.NotNullOrWhiteSpace(value, nameof(value), 256);
        Value = value;
    }

    /// <summary>
    /// 更新语言文本
    /// </summary> 
    public void Update(
        string cultureName,
        string resourceName,
        string name,
        string value
    )
    {
        SetResourceName(resourceName);
        SetCultureName(cultureName);
        SetName(name);
        SetValue(value);
    }
}