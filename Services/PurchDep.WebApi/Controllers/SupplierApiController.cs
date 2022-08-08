using Microsoft.AspNetCore.Mvc;
using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;
using PurchDep.WebApi.Controllers.Base;

using SupplierDom = PurchDep.Domain.Supplier;

namespace PurchDep.WebApi.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierApiController : PurchDepBaseController<Supplier, SupplierDom>
    {
        public SupplierApiController(Service<Supplier, SupplierDom> service) : base(service) { }
    }
}
