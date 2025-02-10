namespace MyCompanyName.MyProjectName.MyModuleName.EntityFrameworkCore
{
    public class MyModuleNameModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public MyModuleNameModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}