using Volo.Abp.Auditing;

namespace Lion.AbpPro.EntityFrameworkCore.Mysql;

public class EfCoreBulkOperationProvider : IEfCoreBulkOperationProvider, ITransientDependency
{
    /// <summary>
    /// 批量新增
    /// </summary>
    ///<remarks>
    /// <para>
    /// - mysql启用:SET GLOBAL local_infile = true;
    /// </para>
    /// <para>
    /// - 数据库连接字符串需要加上：AllowLoadLocalInfile=true
    /// </para>
    /// - abp的审计字段需要手动赋值，比如创建人,创建时间,或者使用AuditPropertySetter
    /// <para>
    /// - 只支持单表,比如有一个Blog表和Post表一对多关系,需要调用两次 InsertManyAsync
    /// </para>
    /// </remarks>
    public virtual async Task InsertManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository, IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken)
        where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        var dbContext = await repository.GetDbContextAsync();
        var dbTransaction = dbContext.Database.CurrentTransaction?.GetDbTransaction();
        await dbContext.BulkInsertAsync(entities, dbTransaction as MySqlTransaction, cancellationToken);
        if (autoSave)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task UpdateManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository, IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken)
        where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        await repository.UpdateManyAsync(entities, autoSave, cancellationToken);
    }

    public virtual async Task DeleteManyAsync<TDbContext, TEntity>(IEfCoreRepository<TEntity> repository, IEnumerable<TEntity> entities, bool autoSave, CancellationToken cancellationToken)
        where TDbContext : IEfCoreDbContext where TEntity : class, IEntity
    {
        await repository.DeleteManyAsync(entities, autoSave, cancellationToken);
    }
}