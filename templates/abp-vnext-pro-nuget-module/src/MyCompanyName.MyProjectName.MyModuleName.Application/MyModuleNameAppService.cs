namespace MyCompanyName.MyProjectName.MyModuleName
{
    public abstract class MyModuleNameAppService : ApplicationService
    {
        protected MyModuleNameAppService()
        {
            LocalizationResource = typeof(MyModuleNameResource);
            ObjectMapperContext = typeof(MyModuleNameApplicationModule);
        }
    }
}
