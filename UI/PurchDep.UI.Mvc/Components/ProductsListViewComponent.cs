using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.UI.Mvc.Components
{
    public class ProductsListViewComponent : ViewComponent
    {
        private readonly IService<Product> _service;

        public ProductsListViewComponent(IService<Product> service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await _service.GetAllAsync();

            var top5 = res.Take(5);

            return View(top5);
        }
    }
}
