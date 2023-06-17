namespace MyCompanyName.MyProjectName.MyModuleName.EntityFrameworkCore
{
    [ConnectionStringName(MyModuleNameDbProperties.ConnectionStringName)]
    public class MyModuleNameDbContext : AbpDbContext<MyModuleNameDbContext>, IMyModuleNameDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        
        public MyModuleNameDbContext(DbContextOptions<MyModuleNameDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureMyModuleName();
        }
    }
}