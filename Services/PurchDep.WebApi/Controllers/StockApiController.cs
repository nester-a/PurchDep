using Microsoft.AspNetCore.Mvc;
using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Base.Web;

using StockDom = PurchDep.Domain.Stock;

namespace PurchDep.WebApi.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockApiController : PurchDepControllerBase<Stock, StockDom>
    {
        public StockApiController(Service<Stock, StockDom> service) : base(service) { }
    }
}
