namespace MyCompanyName.MyProjectName.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class MyProjectNameController : AbpController
    {
        protected MyProjectNameController()
        {
            LocalizationResource = typeof(MyProjectNameResource);
        }
    }
}