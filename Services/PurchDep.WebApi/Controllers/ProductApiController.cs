using Microsoft.AspNetCore.Mvc;
using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Base.Web;

using ProductDom = PurchDep.Domain.Product;

namespace PurchDep.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : PurchDepControllerBase<Product, ProductDom>
    {
        public ProductApiController(Service<Product, ProductDom> service) : base(service) { }
    }
}
