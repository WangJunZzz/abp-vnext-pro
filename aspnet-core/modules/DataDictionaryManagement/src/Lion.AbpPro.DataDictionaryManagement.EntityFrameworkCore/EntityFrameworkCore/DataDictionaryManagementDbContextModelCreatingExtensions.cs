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
                
                // 租户id
                b.Property(e => e.TenantId)
                    .HasComment("租户id");
                
                // 字典编码 - 必填，最大长度64
                b.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(DataDictionaryMaxLengths.Code)
                    .HasComment("字典编码");
                
                // 显示名 - 必填，最大长度64
                b.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(DataDictionaryMaxLengths.DisplayText)
                    .HasComment("显示名");
                
                // 描述 - 必填，最大长度1024
                b.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(DataDictionaryMaxLengths.Description)
                    .HasComment("描述");
                
                // 字典明细集合 - 一对多关系，外键必填
                b.HasMany(e => e.Details).WithOne().HasForeignKey(uc => uc.DataDictionaryId).HasConstraintName("FK_DictDetail_DictId").IsRequired();
                b.ConfigureByConvention();
            });

            builder.Entity<DataDictionaryDetail>(b =>
            {
                b.ToTable(DataDictionaryManagementDbProperties.DbTablePrefix + "DataDictionaryDetails", DataDictionaryManagementDbProperties.DbSchema);
                
                // 所属字典Id - 必填
                b.Property(e => e.DataDictionaryId)
                    .IsRequired()
                    .HasComment("所属字典Id");
                
                // 字典明细编码 - 最大长度64
                b.Property(e => e.Code)
                    .HasMaxLength(DataDictionaryMaxLengths.Code)
                    .HasComment("字典明细编码");
                
                // 展现列表时排序用 - 必填
                b.Property(e => e.Order)
                    .IsRequired()
                    .HasComment("展现列表时排序用");
                
                // 英文显示名 - 最大长度64
                b.Property(e => e.DisplayText)
                    .HasMaxLength(DataDictionaryMaxLengths.DisplayText)
                    .HasComment("英文显示名");
                
                // 描述 - 最大长度1024
                b.Property(e => e.Description)
                    .HasMaxLength(DataDictionaryMaxLengths.Description)
                    .HasComment("描述");
                
                // 启/停用(默认启用) - 必填
                b.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasComment("启/停用(默认启用)");
                
                b.ConfigureByConvention();
            });
        }
    }
}