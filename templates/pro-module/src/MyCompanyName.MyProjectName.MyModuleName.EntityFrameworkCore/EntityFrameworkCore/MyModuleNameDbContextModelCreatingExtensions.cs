namespace MyCompanyName.MyProjectName.MyModuleName.EntityFrameworkCore
{
    public static class MyModuleNameDbContextModelCreatingExtensions
    {
        public static void ConfigureMyModuleName(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
          
        }
    }
}