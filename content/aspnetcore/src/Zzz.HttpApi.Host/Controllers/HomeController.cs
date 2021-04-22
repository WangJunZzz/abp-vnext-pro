using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Users;

namespace Zzz.Controllers
{
    public class HomeController : AbpController
    {

        public ActionResult Index()
        {
            return Redirect("~/swagger/index.html");
        }
    }
}
