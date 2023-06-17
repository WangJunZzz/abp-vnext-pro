namespace MyCompanyName.MyProjectName.MyModuleName.EntityFrameworkCore
{
    [ConnectionStringName(MyModuleNameDbProperties.ConnectionStringName)]
    public interface IMyModuleNameDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}