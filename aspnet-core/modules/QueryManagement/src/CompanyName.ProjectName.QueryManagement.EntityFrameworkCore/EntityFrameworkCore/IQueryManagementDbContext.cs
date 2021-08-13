using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CompanyName.ProjectName.QueryManagement.EntityFrameworkCore
{
    [ConnectionStringName(QueryManagementDbProperties.ConnectionStringName)]
    public interface IQueryManagementDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}