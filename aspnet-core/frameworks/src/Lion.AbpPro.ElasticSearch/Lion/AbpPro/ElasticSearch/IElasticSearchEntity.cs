namespace Lion.AbpPro.ElasticSearch;


public interface IElasticSearchEntity
{
    /// <summary>
    /// 主键Id
    /// </summary>
    Guid Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreationTime { get; set; }
}