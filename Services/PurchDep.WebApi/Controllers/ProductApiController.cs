using Microsoft.AspNetCore.Mvc;
using PurchDep.Dal.Entities;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Services;
using PurchDep.WebApi.Controllers.Base;

using ProductDom = PurchDep.Domain.Product;

namespace PurchDep.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : PurchDepBaseController<Product, ProductDom>
    {
        public ProductApiController(Service<Product, ProductDom> service) : base(service) { }
    }
}
