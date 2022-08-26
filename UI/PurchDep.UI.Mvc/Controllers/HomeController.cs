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
            var products = CreateModel(_productService.GetAll());

            var suppliers = CreateModel(_supplierService.GetAll());

            var stocks = CreateModel(_stockService.GetAll());

            var model = CreateListModel(products, suppliers, stocks);

            return View(model);
        }

        private IndexNamedEnitiesListModel CreateListModel(params IndexNamedEntityModel[] items)
        {
            List<IndexNamedEntityModel> list = new();

            foreach (var item in items)
            {
                list.Add(item);
            }
            IndexNamedEnitiesListModel model = new();
            model.Items = list;

            return model;
        }

        private IndexNamedEntityModel CreateModel(IEnumerable<INamedEntity> items)
        {
            var model = new IndexNamedEntityModel();

            switch (items)
            {
                case IEnumerable<Product>:
                    model.NamedEntityName = "Products";
                    break;
                case IEnumerable<Supplier>:
                    model.NamedEntityName = "Suppliers";
                    break;
                case IEnumerable<Stock>:
                    model.NamedEntityName = "Stocks";
                    break;
                default:
                    model.NamedEntityName = "Unknown Type";
                    break;
            }
            model.Items = items;

            return model;
        }
    }
}
