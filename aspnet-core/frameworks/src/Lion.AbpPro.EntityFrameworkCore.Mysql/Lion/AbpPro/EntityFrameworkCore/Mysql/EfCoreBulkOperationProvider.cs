using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Repositories.Dapper;

namespace Lion.AbpPro.EntityFrameworkCore.Mysql;

public class EfCoreBulkOperationProvider : IEfCoreBulkOperationProvider, ITransientDependency
{
    private readonly ILogger<EfCoreBulkOperationProvider> _logger;
    public EfCoreBulkOperationProvider(ILogger<EfCoreBulkOperationProvider> logger)
    {
        _logger = logger;
    }

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
        var dbConnection =  dbContext.Database.GetDbConnection();
        var count = await dbConnection.BulkInsertAsync(dbContext, entities, dbTransaction as MySqlTransaction);
        _logger.LogInformation($"批量新增{count}条数据成功");
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