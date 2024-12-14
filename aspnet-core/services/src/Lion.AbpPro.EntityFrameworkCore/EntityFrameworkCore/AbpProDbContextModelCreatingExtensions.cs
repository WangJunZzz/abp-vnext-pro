using Humanizer;
using Lion.AbpPro.Books;

namespace Lion.AbpPro.EntityFrameworkCore
{
    public static class AbpProDbContextModelCreatingExtensions
    {
        public static void ConfigureAbpPro(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<Book>(b =>
            {
                b.ToTable(AbpProConsts.DbTablePrefix + nameof(Book).Pluralize());
                b.Property(e => e.No).IsRequired().HasMaxLength(128).HasComment("编号");
                b.Property(e => e.Name).IsRequired().HasMaxLength(128).HasComment("名称");
                b.Property(e => e.Price).IsRequired().HasPrecision(6,2).HasComment("价格");
                b.Property(e => e.Remark).HasComment("备注");
                b.Property(e => e.BookType).HasComment("类型");
                b.ConfigureByConvention();
            });
        }
    }
}