using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.UI.Mvc.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly IService<Supplier> _service;

        public SuppliersController(IService<Supplier> service)
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
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
