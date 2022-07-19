namespace Lion.AbpPro.Users;

public interface IBulkImportUserRepository : ITransientDependency
{
    Task BulkInsertAsync(List<IdentityUser> identityUsers);
}