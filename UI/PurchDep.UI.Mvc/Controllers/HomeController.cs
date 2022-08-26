using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.UI.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> service;

        public HomeController(IService<Product> service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            var res = service.GetAll();
            return View(res);
        }
    }
}
