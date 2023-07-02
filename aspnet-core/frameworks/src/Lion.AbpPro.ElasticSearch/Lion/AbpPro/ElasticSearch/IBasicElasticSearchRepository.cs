using Lion.AbpPro.ElasticSearch.Exceptions;

namespace Lion.AbpPro.ElasticSearch;

public interface IBasicElasticSearchRepository<TEntity> where TEntity : class, IElasticSearchEntity
{
    /// <summary>
    /// 根据id查询实体
    /// </summary>
    Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据id查询实体
    /// </summary>
    /// <exception cref="AbpProElasticSearchEntityNotFoundException"></exception>
    /// <returns>如果没有查询到,会抛异常</returns>
    Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    /// 新增数据时，如果文档的唯一id在索引里已存在，那么会更新掉原数据
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量新增数据时，如果文档的唯一id在索引里已存在，那么会更新掉原数据
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据Id删除实体
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    Task UpdateAsync(TEntity TEntity);

    /// <summary>
    /// 根据Id更新实体
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Tuple<long, IList<TEntity>>> PageAsync(List<Func<QueryContainerDescriptor<TEntity>, QueryContainer>> predicates, int pageIndex = 1, int pageSize = 10);
}