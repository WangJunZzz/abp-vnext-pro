namespace Lion.AbpPro.FileManagement;

public class FileManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IGuidGenerator _guidGenerator;

    public FileManagementDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    public Task SeedAsync(DataSeedContext context)
    {
        /* Instead of returning the Task.CompletedTask, you can insert your test data
         * at this point!
         */

        using (_currentTenant.Change(context?.TenantId))
        {
            return Task.CompletedTask;
        }
    }
}