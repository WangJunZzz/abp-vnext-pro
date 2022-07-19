namespace Lion.AbpPro.EntityFrameworkCore;

public class BulkImportUserRepository:IBulkImportUserRepository
{
    private readonly IDbContextProvider<AbpProDbContext> _contextProvider;

    public BulkImportUserRepository(IDbContextProvider<AbpProDbContext> contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public async Task BulkInsertAsync(List<IdentityUser> identityUsers)
    {
        // TODO 这个地方创建人和创建时间需要手动赋值。
        var context = await _contextProvider.GetDbContextAsync();
        await context.BulkInsertAsync(identityUsers, context.Database.CurrentTransaction.GetDbTransaction() as MySqlTransaction);
    }
}