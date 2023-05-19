namespace Lion.AbpPro.LanguageManagement.EntityFrameworkCore
{
    public static class LanguageManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureLanguageManagement(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Language>(b =>
            {
                b.ToTable(LanguageManagementDbProperties.DbTablePrefix + "Languages");
                b.Property(e => e.CultureName).IsRequired().HasMaxLength(128).HasComment("语言名称");
                b.Property(e => e.UiCultureName).IsRequired().HasMaxLength(128).HasComment("Ui语言名称");
                b.Property(e => e.DisplayName).IsRequired().HasMaxLength(128).HasComment("显示名称");
                b.Property(e => e.FlagIcon).HasMaxLength(128).HasComment("图标");
                b.Property<bool>(x => x.IsEnabled).IsRequired();
                b.HasIndex(e => e.CultureName).IsUnique();
                b.ConfigureByConvention();
            });
            
            builder.Entity<LanguageText>(b =>
            {
                b.ToTable(LanguageManagementDbProperties.DbTablePrefix + "LanguageTexts");
                b.Property(e => e.ResourceName).IsRequired().HasMaxLength(128).HasComment("资源名称");
                b.Property(e => e.CultureName).IsRequired().HasMaxLength(128).HasComment("语言名称");
                b.Property(e => e.Name).IsRequired().HasMaxLength(256).HasComment("名称");
                b.Property(e => e.Value).IsRequired().HasMaxLength(256).HasComment("值");
                b.HasIndex(x => new { x.TenantId, x.ResourceName, x.CultureName });
                b.ConfigureByConvention();
            });
        }
    }
}