using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;
using PurchDep.UI.Mvc.Models;

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

        public IActionResult AddProduct(int id)
        {
            var model = new AddSupplierProductModel() { SupplierId = id };
            return View("AddProduct", model );
        }

        [HttpPost]
        public IActionResult AddProduct(AddSupplierProductModel model)
        {
            if (model is null) throw new ArgumentNullException();
            var supplier = _supplierService.Get((int)model.SupplierId!);
            if (model.NewProduct)
            {
                var productDom = new Product()
                {
                    Name = model.NewProductName!,
                };
                var res = _productService.Add(productDom);

                var supProduct = new SuppliersProduct()
                {
                    Id = res.Id,
                    Name = res.Name,
                    SupplierId = (int)model.SupplierId,
                    SuppliersPrice = model.Price,
                };
                supplier.SuppliersProducts.Add(supProduct);
            }
            else
            {
                var productDom = _productService.Get((int)model.ProductId!);
                var supProduct = new SuppliersProduct()
                {
                    Id = productDom.Id,
                    Name = productDom.Name,
                    SupplierId = (int)model.SupplierId,
                    SuppliersPrice = model.Price,
                };
                supplier.SuppliersProducts.Add(supProduct);
            }

            _supplierService.Update(supplier.Id, supplier);
            return RedirectToAction("Details", new { id = supplier.Id });
        }

        [HttpPost]
        public IActionResult UpdateProduct(UpdateSuppliersProductModel model)
        {
            var supplier = _supplierService.Get(model.SupplierId);
            var productDom = supplier.SuppliersProducts.FirstOrDefault(p => p.Id == model.ProductId && p.SupplierId == model.SupplierId);
            productDom!.SuppliersPrice = model.NewPrice;

            _supplierService.Update(supplier.Id, supplier);
            return RedirectToAction("Details", new { id = supplier.Id });
        }

        public IActionResult UpdateProduct(int supplierId, int productId)
        {
            var model = new UpdateSuppliersProductModel() { ProductId = productId, SupplierId = supplierId };
            return View("UpdateProduct", model);
        }

        [HttpPost]
        public IActionResult RemoveProduct(int supplierId, int productId)
        {
            var supplier = _supplierService.Get(supplierId);
            supplier.SuppliersProducts.RemoveWhere(p => p.Id == productId && p.SupplierId == supplierId);

            _supplierService.Update(supplierId, supplier);
            return RedirectToAction("Details", new { id = supplierId });
        }
    }
}
