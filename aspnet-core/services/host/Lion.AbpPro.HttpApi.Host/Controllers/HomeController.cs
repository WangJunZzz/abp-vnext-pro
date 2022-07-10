namespace Lion.AbpPro.Controllers
{
    public class HomeController : AbpController
    {
        public ActionResult Index()
        {
            return Redirect("/Login");
        }
    }
}
