namespace MyCompanyName.MyProjectName.Data
{
    /* This is used if database provider does't define
     * IMyProjectNameDbSchemaMigrator implementation.
     */
    public class NullMyProjectNameDbSchemaMigrator : IMyProjectNameDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}