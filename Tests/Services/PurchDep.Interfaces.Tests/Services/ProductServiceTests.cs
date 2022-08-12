using Moq;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.Interfaces.Tests.Services.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ProductDal = PurchDep.Dal.Entities.Product;

using ProductDom = PurchDep.Domain.Product;

namespace PurchDep.Interfaces.Tests.Services
{
    [Collection("Service collection")]
    public class ProductServiceTests
    {
        ProductService _service;
        Mock<ProductRepository> _productRepositoryMock;
        Mock<ProductMappingService> _productMappingServiceMock;
        public ProductServiceTests(ServiceFixture fixture)
        {
            _productRepositoryMock = fixture.ProductRepositoryMock;
            _productMappingServiceMock = fixture.ProductMappingServiceMock;
            _service = new(fixture.ProductRepositoryMockObject, fixture.ProductMappingServiceMockObject);
        }

        [Fact]
        public void AddNewItem()
        {
            var res = _service.Add(new ProductDom());

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _productRepositoryMock.Verify(prm => prm.Add(It.IsAny<ProductDal>()));
            _productMappingServiceMock.Verify(pms => pms.Map(It.IsAny<ProductDom>()));
        }
    }
}
