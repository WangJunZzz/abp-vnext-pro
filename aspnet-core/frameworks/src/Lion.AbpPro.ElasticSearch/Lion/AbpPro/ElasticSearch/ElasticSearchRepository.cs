using Lion.AbpPro.ElasticSearch.Exceptions;

namespace Lion.AbpPro.ElasticSearch;

public abstract class ElasticSearchRepository<TEntity> : IBasicElasticSearchRepository<TEntity>
    where TEntity : class, IElasticSearchEntity
{
    protected abstract string IndexName { get; }

    private readonly IElasticsearchProvider _elasticsearchProvider;

    protected ElasticSearchRepository(IElasticsearchProvider elasticsearchProvider)
    {
        _elasticsearchProvider = elasticsearchProvider;
    }


    protected IElasticClient Client => _elasticsearchProvider.GetClient();

    /// <summary>
    /// 根据id查询实体
    /// </summary>
    public virtual async Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Client.GetAsync<TEntity>(id, e => e.Index(IndexName), cancellationToken);
        if (!result.IsValid)
        {
            return null;
        }

        return result.Source;
    }

    /// <summary>
    /// 根据id查询实体
    /// </summary>
    /// <exception cref="AbpProElasticSearchEntityNotFoundException"></exception>
    /// <returns>如果没有查询到,会抛异常</returns>
    public virtual async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Client.GetAsync<TEntity>(id, e => e.Index(IndexName), cancellationToken);
        if (!result.IsValid)
        {
            throw new AbpProElasticSearchEntityNotFoundException(innerException: result.OriginalException);
        }

        return result.Source;
    }

    /// <summary>
    /// 新增数据时，如果文档的唯一id在索引里已存在，那么会更新掉原数据
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = await Client.IndexAsync(entity, x => x.Index(IndexName), cancellationToken);
        if (!result.IsValid)
        {
            throw new AbpProElasticSearchException(innerException: result.OriginalException);
        }
    }

    /// <summary>
    /// 批量新增数据时，如果文档的唯一id在索引里已存在，那么会更新掉原数据
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    public async Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        // var result = await Client.BulkAsync(b => b.Index(IndexName).IndexMany(entities), cancellationToken);
        var result = await Client.IndexManyAsync(entities, IndexName, cancellationToken);
        if (!result.IsValid)
        {
            throw new AbpProElasticSearchException(innerException: result.OriginalException);
        }
    }

    /// <summary>
    /// 根据Id删除实体
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await Client.DeleteAsync<TEntity>(id, x => x.Index(IndexName), cancellationToken);
        if (!result.IsValid)
        {
            throw new AbpProElasticSearchException(innerException: result.OriginalException);
        }
    }

    /// <summary>
    /// 根据Id更新实体
    /// </summary>
    /// <exception cref="AbpProElasticSearchException"></exception>
    public virtual async Task UpdateAsync(TEntity TEntity)
    {
        var result = await Client.UpdateAsync<TEntity>(TEntity.Id, x => x.Index(IndexName).Doc(TEntity));
        if (!result.IsValid)
        {
            throw new UserFriendlyException(result.OriginalException.Message);
        }
    }


    public async Task<Tuple<long, IList<TEntity>>> PageAsync(List<Func<QueryContainerDescriptor<TEntity>, QueryContainer>> predicates, int pageIndex = 1, int pageSize = 10)
    {
        predicates ??= new List<Func<QueryContainerDescriptor<TEntity>, QueryContainer>>();
        var query = await Client.SearchAsync<TEntity>(x => x.Index(IndexName)
            .Query(q => q.Bool(qb => qb.Filter(predicates)))
            .From((pageIndex - 1) * pageSize)
            .Size(pageSize)
            .Sort(s => s.Descending(v => v.CreationTime)));
        return new Tuple<long, IList<TEntity>>(query.Total, query.Documents.ToList());
    }
}