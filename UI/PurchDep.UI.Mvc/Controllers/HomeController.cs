using Microsoft.AspNetCore.Mvc;

namespace PurchDep.UI.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
