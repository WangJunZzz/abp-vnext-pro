using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CompanyName.ProjectName.QueryManagement.EntityFrameworkCore
{
    [ConnectionStringName(QueryManagementDbProperties.ConnectionStringName)]
    public class QueryManagementDbContext : AbpDbContext<QueryManagementDbContext>, IQueryManagementDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public QueryManagementDbContext(DbContextOptions<QueryManagementDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureQueryManagement();
        }
    }
}