namespace Lion.AbpPro.Data
{
    public interface IAbpProDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
