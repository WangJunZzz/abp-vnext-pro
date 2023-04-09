namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore
{
    [ConnectionStringName(LanguageManagementDbProperties.ConnectionStringName)]
    public interface ILanguageManagementDbContext : IEfCoreDbContext
    {
        DbSet<Language> Languages { get; set; }
        DbSet<LanguageText> LanguageTexts { get; set; }
    }
}