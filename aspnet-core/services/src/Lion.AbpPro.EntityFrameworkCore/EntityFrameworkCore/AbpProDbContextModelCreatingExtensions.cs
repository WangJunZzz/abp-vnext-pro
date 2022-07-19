namespace Lion.AbpPro.EntityFrameworkCore
{
    public static class AbpProDbContextModelCreatingExtensions
    {
        public static void ConfigureAbpPro(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(AbpProConsts.DbTablePrefix + "YourEntities", AbpProConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}