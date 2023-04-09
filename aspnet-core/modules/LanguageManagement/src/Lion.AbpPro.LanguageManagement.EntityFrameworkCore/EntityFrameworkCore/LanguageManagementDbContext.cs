namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore
{
    [ConnectionStringName(LanguageManagementDbProperties.ConnectionStringName)]
    public class LanguageManagementDbContext : AbpDbContext<LanguageManagementDbContext>, ILanguageManagementDbContext
    {
        public DbSet<Language> Languages { get; set; }
        
        public DbSet<LanguageText> LanguageTexts { get; set; }

        public LanguageManagementDbContext(DbContextOptions<LanguageManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureLanguageManagement();
        }
    }
}