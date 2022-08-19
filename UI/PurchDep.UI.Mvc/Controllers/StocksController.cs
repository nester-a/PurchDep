using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;
using PurchDep.UI.Mvc.Models;

namespace PurchDep.UI.Mvc.Controllers
{
    public class StocksController : Controller
    {
        private readonly IService<Stock> _stockService;
        private readonly IService<Supplier> _supplierService;
        private readonly IService<Product> _productService;

        public StocksController(IService<Stock> stockService, IService<Supplier> supplierService, IService<Product> productService)
        {
            _stockService = stockService;
            _supplierService = supplierService;
            _productService = productService;
        }


        public IActionResult Index()
        {
            var items = _stockService.GetAll();
            return View(items);
        }
        public IActionResult Details(int id)
        {
            var item = _stockService.Get(id);
            return View(item);
        }
        public IActionResult Delete(int id)
        {
            _stockService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            return View("Update", new Stock());
        }

        public IActionResult Update(int? id)
        {
            if (id is null) return View(new Stock());

            var supplier = _stockService.Get((int)id);
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
            if (item.Id == 0) _stockService.Add(item);
            else _stockService.Update(item.Id, item);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddProduct(int id)
        {
            var model = new AddStocksProductModel() { StockId = id };
            return View("AddProduct", model);
        }

        [HttpPost]
        public IActionResult AddProduct(AddStocksProductModel model)
        {
            if (model is null) throw new ArgumentNullException();
            var stock = _stockService.Get(model.StockId);
            var product = _productService.Get(model.ProductId);
            var supplier = _supplierService.Get(model.SupplierId);
            var supplierProduct = supplier.SuppliersProducts.FirstOrDefault(p => p.Id == product.Id);

            var result = new StocksProduct()
            {
                Id = product.Id,
                Name = product.Name,
                SupplierId = supplier.Id,
                SuppliersPrice = supplierProduct!.SuppliersPrice,
                StockId = model.StockId,
                Quantity = model.Quantity,
            };
            stock.StocksProducts.Add(result);

            _stockService.Update(stock.Id, stock);
            return RedirectToAction("Details", new { id = stock.Id });
        }


        [HttpPost]
        public IActionResult UpdateProduct(UpdateStocksProductModel model)
        {
            var stock = _stockService.Get(model.StockId);
            var productDom = stock.StocksProducts.FirstOrDefault(p => p.Id == model.ProductId && p.SupplierId == model.SupplierId && p.StockId == model.StockId);
            productDom!.Quantity = model.NewQuantity;

            _stockService.Update(stock.Id, stock);
            return RedirectToAction("Details", new { id = stock.Id });
        }

        public IActionResult UpdateProduct(int stockId, int supplierId, int productId)
        {
            var model = new UpdateStocksProductModel() { ProductId = productId, SupplierId = supplierId,  StockId = stockId };
            return View("UpdateProduct", model);
        }

        [HttpPost]
        public IActionResult RemoveProduct(int stockId, int supplierId, int productId)
        {
            var stock = _stockService.Get(supplierId);
            stock.StocksProducts.RemoveWhere(p => p.Id == productId && p.SupplierId == supplierId && p.StockId == stockId);

            _stockService.Update(stockId, stock);
            return RedirectToAction("Details", new { id = stockId });
        }
    }
}
