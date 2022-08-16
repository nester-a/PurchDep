using Moq;
using Moq.Protected;
using PurchDep.Domain;
using PurchDep.WebApi.Clients.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PurchDep.WebApi.Clients.Tests.Products
{
    public class ProductsClientTests
    {
        public ProductsClientTests()
        {

        }

        [Fact]
        public async void Test1()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(new Product())),
            };

            handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(response);
            

            var httpClient = new HttpClient(handlerMock.Object);

            var client = new ProductsClient(httpClient);

            var result = client.Get(1);

            Assert.NotNull(result);
            handlerMock.Protected().Setup<HttpResponseMessage>("Send", ItExpr.IsAny<HttpRequestMessage>());

        }
    }
}
