using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Lion.AbpPro.AuditLoggingManagement.HttpApi.Host.EntityFrameworkCore
{
    [ConnectionStringName("AbpAuditLogging")]
    public class AuditLoggingManagementHttpApiHostMigrationsDbContext : AbpDbContext<AuditLoggingManagementHttpApiHostMigrationsDbContext>
    {
        public DbSet<AuditLog> AuditLogs { get; set; }

        public AuditLoggingManagementHttpApiHostMigrationsDbContext(DbContextOptions<AuditLoggingManagementHttpApiHostMigrationsDbContext> options)
       : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureAuditLogging();
        }
    }
}
