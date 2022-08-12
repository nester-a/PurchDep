using Moq;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.Interfaces.Tests.Services.Fixtures;
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
            _productRepositoryMock.Setup(o => o.Add(It.IsAny<ProductDal>())).Returns(new ProductDal() { Id = 1, Name = "Added_FromMockRepository_ProductObj" });
            var res = _service.Add(new ProductDom());

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _productRepositoryMock.Verify(prm => prm.Add(It.IsAny<ProductDal>()));
            _productMappingServiceMock.Verify(pms => pms.Map(It.IsAny<ProductDom>()));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            //Assert.Throws<ArgumentNullException>(() => _service.Add(null!));
            //_productRepositoryMock.Verify(prm => prm.Add(null!));

            //_productRepositoryMock.Setup(o => o.Add(null!)).Throws(new ArgumentNullException());
            //bool cathced = false;
            //try
            //{
            //    _service.Add(null);
            //}
            //catch (ArgumentNullException e)
            //{
            //    cathced = true;
            //    Assert.True(e is not null);
            //}
            //Assert.True(cathced);
        }
    }
}
