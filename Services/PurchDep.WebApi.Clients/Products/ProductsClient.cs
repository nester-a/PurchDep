using PurchDep.Domain;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Base.Web;

namespace PurchDep.WebApi.Clients.Products
{
    public class ProductsClient : ClientBase<Product>, IService<Product>
    {
        public ProductsClient(HttpClient client) : base(client, "api/products") { }
    }
}
