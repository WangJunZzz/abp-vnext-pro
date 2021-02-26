using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zzz.Dic;

namespace Zzz.EntityFrameworkCore
{
    public static class ZzzDbContextModelCreatingExtensions
    {
        public static void ConfigureZzz(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(ZzzConsts.DbTablePrefix + "YourEntities", ZzzConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            builder.Entity<DataDictionary>(e =>
            {
                e.ToTable(ZzzConsts.ZzzDbTablePrefix + nameof(DataDictionary));
                e.Property(e => e.IsDeleted).HasDefaultValue(false);
                e.ConfigureByConvention();
            });
            builder.Entity<DataDictionaryDetail>(e =>
            {
                e.ToTable(ZzzConsts.ZzzDbTablePrefix + nameof(DataDictionaryDetail));
                e.Property(e => e.IsDeleted).HasDefaultValue(false);
            });
        }
    }
}