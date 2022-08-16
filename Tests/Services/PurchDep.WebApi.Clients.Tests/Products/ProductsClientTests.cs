using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PurchDep.WebApi.Clients.Tests.Products
{
    public class ProductsClientTests
    {
        public ProductsClientTests()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
        }
    }
}
