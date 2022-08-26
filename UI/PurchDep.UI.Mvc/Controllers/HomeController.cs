using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Domain.Base.Core;
using PurchDep.Interfaces.Base.Services;
using PurchDep.UI.Mvc.Models;

namespace PurchDep.UI.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Stock> _stockService;
        private readonly IService<Supplier> _supplierService;
        private readonly IService<Product> _productService;

        public HomeController(IService<Stock> stockService, IService<Supplier> supplierService, IService<Product> productService)
        {
            _stockService = stockService;
            _supplierService = supplierService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            IndexNamedEnitiesListModel model = new();

            List<IndexNamedEntityModel> models = new();
            IndexNamedEntityModel products = new IndexNamedEntityModel()
            {
                NamedEntityName = "Products",
            };
            products.Items = _productService.GetAll();

            IndexNamedEntityModel suppliers = new IndexNamedEntityModel()
            {
                NamedEntityName = "Suppliers",
            };
            suppliers.Items = _supplierService.GetAll();

            //IndexNamedEntityModel stocks = new IndexNamedEntityModel()
            //{
            //    NamedEntityName = "Stocks",
            //};
            //stocks.Items = _stockService.GetAll();

            models.Add(products);
            models.Add(suppliers);

            model.Items = models;

            return View(model);
        }
    }
}
