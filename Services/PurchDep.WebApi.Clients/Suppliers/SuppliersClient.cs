using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Base.Web;

namespace PurchDep.WebApi.Clients.Suppliers
{
    public class SuppliersClient : ClientBase<Supplier>, IService<Supplier>
    {
        public SuppliersClient(HttpClient client) : base(client, "api/suppliers") { }
    }
}
