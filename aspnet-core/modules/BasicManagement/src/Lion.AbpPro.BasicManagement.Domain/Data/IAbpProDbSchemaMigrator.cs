namespace Lion.AbpPro.BasicManagement.Data
{
    public interface IAbpProDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
