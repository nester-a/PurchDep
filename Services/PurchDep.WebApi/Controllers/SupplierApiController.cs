using Microsoft.AspNetCore.Mvc;
using PurchDep.Dal.Entities;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Services;
using PurchDep.WebApi.Controllers.Base;

namespace PurchDep.WebApi.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierApiController : PurchDepBaseController<Supplier, ISupplier>
    {
        public SupplierApiController(Service<Supplier, ISupplier> service) : base(service) { }
    }
}
