using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Base.Web;

namespace PurchDep.WebApi.Clients.Products
{
    public class ProductsClient<T> : ClientBase<T>, IService<T> where T : class, IProduct
    {
        public ProductsClient(HttpClient client) : base(client, "api/products") { }
    }
}
