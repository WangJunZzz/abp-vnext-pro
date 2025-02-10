namespace MyCompanyName.MyProjectName
{
    /* Inherit your application services from this class.
     */
    public abstract class MyProjectNameAppService : ApplicationService
    {
        protected MyProjectNameAppService()
        {
            LocalizationResource = typeof(MyProjectNameResource);
        }
    }
}
