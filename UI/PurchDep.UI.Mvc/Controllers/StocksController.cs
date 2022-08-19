using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.UI.Mvc.Controllers
{
    public class StocksController : Controller
    {
        private readonly IService<Stock> _service;

        public StocksController(IService<Stock> service)
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
            return View("Update", new Stock());
        }

        public IActionResult Update(int? id)
        {
            if (id is null) return View(new Stock());

            var supplier = _service.Get((int)id);
            if (supplier is null) return NotFound();

            var item = new Stock()
            {
                Name = supplier.Name,
            };

            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Stock model)
        {
            var item = model;
            if (item.Id == 0) _service.Add(item);
            else _service.Update(item.Id, item);

            return RedirectToAction(nameof(Index));
        }
    }
}
