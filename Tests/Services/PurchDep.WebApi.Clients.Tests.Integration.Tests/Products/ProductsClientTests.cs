using PurchDep.Domain;
using PurchDep.WebApi.Clients.Products;
using PurchDep.WebApi.Clients.Tests.Integration.Tests.Fixtures;
using Xunit;

namespace PurchDep.WebApi.Clients.Tests.Integration.Tests.Products
{
    [Collection("WebApi Client collection")]
    public class ProductsClientTests
    {
        private readonly HostFixture _fixture;
        ProductsClient<Product> _client;

        public ProductsClientTests(HostFixture fixture)
        {
            _fixture = fixture;
            _client = new ProductsClient<Product>(_fixture.HttpClient);
        }

        [Fact]
        public void GetAllTest()
        {
            var res = _client.GetAll();
            Assert.NotNull(res);
        }
    }
}
