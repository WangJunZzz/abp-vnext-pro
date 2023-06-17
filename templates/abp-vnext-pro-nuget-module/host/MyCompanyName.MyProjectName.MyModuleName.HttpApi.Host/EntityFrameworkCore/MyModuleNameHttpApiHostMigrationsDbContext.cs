namespace MyCompanyName.MyProjectName.MyModuleName.EntityFrameworkCore
{
    public class MyModuleNameHttpApiHostMigrationsDbContext : AbpDbContext<MyModuleNameHttpApiHostMigrationsDbContext>
    {
        public MyModuleNameHttpApiHostMigrationsDbContext(DbContextOptions<MyModuleNameHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureMyModuleName();
        }
    }
}
