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
                builder.Entity<DataDictionary>(b =>
                {
                    b.ToTable(DataDictionaryManagementDbProperties.DbTablePrefix + nameof(DataDictionary),
                        DataDictionaryManagementDbProperties.DbSchema);
                    b.ConfigureByConvention();
                });
            });
            
            builder.Entity<DataDictionaryDetail>(b =>
            {
                builder.Entity<DataDictionaryDetail>(b =>
                {
                    b.ToTable(DataDictionaryManagementDbProperties.DbTablePrefix + nameof(DataDictionaryDetail),
                        DataDictionaryManagementDbProperties.DbSchema);
                    b.ConfigureByConvention();
                });
            });
        }
    }
}