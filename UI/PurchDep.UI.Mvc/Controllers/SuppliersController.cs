using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.UI.Mvc.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly IService<Supplier> _supplierService;
        private readonly IService<Product> _productService;

        public SuppliersController(IService<Supplier> supplierService, IService<Product> productService)
        {
            _supplierService = supplierService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var items = _supplierService.GetAll();
            return View(items);
        }
        public IActionResult Details(int id)
        {
            var item = _supplierService.Get(id);
            return View(item);
        }
        public IActionResult Delete(int id)
        {
            _supplierService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View("Update", new Supplier());
        }

        public IActionResult Update(int? id)
        {
            if (id is null) return View(new Supplier());

            var supplier = _supplierService.Get((int)id);
            if (supplier is null) return NotFound();

            var item = new Supplier()
            {
                Name = supplier.Name,
            };

            return View(item);
        }

        [HttpPost]
        public IActionResult Update(Supplier model)
        {
            var item = model;
            if (item.Id == 0) _supplierService.Add(item);
            else _supplierService.Update(item.Id, item);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddSuppliersProduct(int id)
        {
            var model = new SuppliersProduct() { SupplierId = id };
            return View("AddSuppliersProduct", model );
        }

        [HttpPost]
        public IActionResult AddSuppliersProduct(SuppliersProduct supplierProduct)
        {
            if (supplierProduct is null) throw new ArgumentNullException();
            if (supplierProduct.Id == 0)
            {
                var productDom = new Product()
                {
                    Name = supplierProduct.Name,
                };
                var res = _productService.Add(productDom);
                supplierProduct.Id = res.Id;
            }

            var supplier = _supplierService.Get(supplierProduct.SupplierId);
            supplier.SuppliersProducts.Add(supplierProduct);

            _supplierService.Update(supplier.Id, supplier);
            return RedirectToAction(nameof(Index));
        }
    }
}
