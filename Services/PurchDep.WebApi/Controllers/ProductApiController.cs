using Microsoft.AspNetCore.Mvc;
using PurchDep.Dal.Entities;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Services;
using PurchDep.WebApi.Controllers.Base;

namespace PurchDep.WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : PurchDepBaseController<Product, IProduct>
    {
        public ProductApiController(Service<Product, IProduct> service) : base(service) { }
    }
}
