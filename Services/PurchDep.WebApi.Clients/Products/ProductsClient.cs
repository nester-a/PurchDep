using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Base.Web;

namespace PurchDep.WebApi.Clients.Products
{
    public class ProductsClient : ClientBase<IProduct>, IService<IProduct>
    {
        public ProductsClient(HttpClient client) : base(client, "api/products") { }
    }
}
