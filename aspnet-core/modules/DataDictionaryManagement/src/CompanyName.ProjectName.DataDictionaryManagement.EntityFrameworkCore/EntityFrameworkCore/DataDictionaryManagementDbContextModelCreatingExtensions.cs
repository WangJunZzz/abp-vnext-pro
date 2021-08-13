using System;
using CompanyName.ProjectName.DataDictionaryManagement.DataDictionaries.Aggregates;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CompanyName.ProjectName.DataDictionaryManagement.EntityFrameworkCore
{
    public static class DataDictionaryManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureDataDictionaryManagement(
            this ModelBuilder builder,
            Action<DataDictionaryManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DataDictionaryManagementModelBuilderConfigurationOptions(
                DataDictionaryManagementDbProperties.DbTablePrefix,
                DataDictionaryManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);
            
            
      
            
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