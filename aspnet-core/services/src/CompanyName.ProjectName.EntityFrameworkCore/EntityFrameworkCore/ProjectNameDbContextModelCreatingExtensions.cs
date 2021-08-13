using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace CompanyName.ProjectName.EntityFrameworkCore
{
    public static class ProjectNameDbContextModelCreatingExtensions
    {
        public static void ConfigureProjectName(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(ProjectNameConsts.DbTablePrefix + "YourEntities", ProjectNameConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}