using SERVICE.IService;
using System.Web.Mvc;

namespace ShopManager.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IErrorService errorService) : base(errorService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}