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
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View("Update", new Product());
        }

        public IActionResult Update(int? id)
        {
            if(id is null) return View(new Product());

            var product = _service.Get((int)id);
            if (product is null) return NotFound();

            var item = new Product()
            {
                Name = product.Name,
            };

            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Product model)
        {
            var item = model;
            if(item.Id == 0) _service.Add(item);
            else _service.Update(item.Id, item);

            return RedirectToAction(nameof(Index));
        }
    }
}
