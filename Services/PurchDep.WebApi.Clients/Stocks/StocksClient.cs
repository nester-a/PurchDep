using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Base.Web;

namespace PurchDep.WebApi.Clients.Stocks
{
    public class StocksClient : ClientBase<Stock>, IService<Stock>
    {
        public StocksClient(HttpClient client) : base(client, "api/stocks") { }
    }
}
