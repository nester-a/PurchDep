using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.UI.Mvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IService<Product> _service;

        public ProductsController(IService<Product> service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            var items = _service.GetAll();
            return View(items);
        }
        public IActionResult Details(int id)
        {
            var item = _service.Get(id);
            return View(item);
        }
    }
}
