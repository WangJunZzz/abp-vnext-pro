namespace Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore
{
    public static class DataDictionaryManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureDataDictionaryManagement(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<DataDictionary>(b =>
            {
                b.ToTable(DataDictionaryManagementDbProperties.DbTablePrefix + "DataDictionaries", DataDictionaryManagementDbProperties.DbSchema);
                b.HasMany(e => e.Details).WithOne().HasForeignKey(uc => uc.DataDictionaryId).HasConstraintName("FK_DictDetail_DictId").IsRequired();
                b.ConfigureByConvention();
            });

            builder.Entity<DataDictionaryDetail>(b =>
            {
                b.ToTable(DataDictionaryManagementDbProperties.DbTablePrefix + "DataDictionaryDetails", DataDictionaryManagementDbProperties.DbSchema);
                b.ConfigureByConvention();
            });
        }
    }
}