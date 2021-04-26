using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CompanyNameProjectName.EntityFrameworkCore
{
    public static class CompanyNameProjectNameDbContextModelCreatingExtensions
    {
        public static void ConfigureCompanyNameProjectName(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(CompanyNameProjectNameConsts.DbTablePrefix + "YourEntities", CompanyNameProjectNameConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}