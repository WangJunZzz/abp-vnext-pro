namespace Lion.AbpPro.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AbpProController : AbpController
    {
        protected AbpProController()
        {
            LocalizationResource = typeof(AbpProResource);
        }
    }
}