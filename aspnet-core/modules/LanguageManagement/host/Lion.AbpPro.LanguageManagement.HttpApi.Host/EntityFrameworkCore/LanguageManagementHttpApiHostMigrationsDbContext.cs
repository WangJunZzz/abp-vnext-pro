namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore
{
    public class LanguageManagementHttpApiHostMigrationsDbContext : AbpDbContext<LanguageManagementHttpApiHostMigrationsDbContext>
    {
        public LanguageManagementHttpApiHostMigrationsDbContext(DbContextOptions<LanguageManagementHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureLanguageManagement();
        }
    }
}
