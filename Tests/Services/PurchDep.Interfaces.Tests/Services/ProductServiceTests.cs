using Moq;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.Interfaces.Tests.Services.Fixtures;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ProductDal = PurchDep.Dal.Entities.Product;

using ProductDom = PurchDep.Domain.Product;

namespace PurchDep.Interfaces.Tests.Services
{
    [Collection("Service collection")]
    public class ProductServiceTests
    {
        Mock<ProductRepository> _repositoryMock;
        Mock<ProductMappingService> _mappingServiceMock;
        public ProductServiceTests(ServiceFixture fixture)
        {
            _repositoryMock = fixture.ProductRepositoryMock;
            _mappingServiceMock = fixture.ProductMappingServiceMock;
        }

        [Fact]
        public void AddNewItem()
        {
            _mappingServiceMock.Setup(o => o.Map(It.IsAny<ProductDom>())).Returns(new ProductDal() { Id = 1, Name = "Mapped_FromMockMapper_ProductDalObj" });
            _repositoryMock.Setup(o => o.Add(It.IsAny<ProductDal>())).Returns(new ProductDal() { Id = 1, Name = "Added_FromMockRepository_ProductObj" });
            
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.Add(new ProductDom());
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _repositoryMock.Verify(prm => prm.Add(It.IsAny<ProductDal>()));
            _mappingServiceMock.Verify(pms => pms.Map(It.IsAny<ProductDom>()));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            _mappingServiceMock.Setup(o => o.Map(default(ProductDom)!)).Returns(default(ProductDal)!);
            _repositoryMock.Setup(o => o.Add(default(ProductDal)!)).Throws(new ArgumentNullException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentNullException>(() => service.Add(null!));
            _mappingServiceMock.Verify(pmsm => pmsm.Map(default(ProductDom)!));
            _repositoryMock.Verify(prm => prm.Add(null!));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentException()
        {
            _mappingServiceMock.Setup(o => o.Map(It.IsAny<ProductDom>())).Returns(TestData.TestData.ProductDal_1);
            _repositoryMock.Setup(o => o.Add(TestData.TestData.ProductDal_1)).Throws(new ArgumentException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Add(new ProductDom()));
            _mappingServiceMock.Verify(pmsm => pmsm.Map(It.IsAny<ProductDom>()));
            _repositoryMock.Verify(prm => prm.Add(TestData.TestData.ProductDal_1));
        }

        [Fact]
        public async Task AddNewItemAsync()
        {
            _mappingServiceMock.Setup(o => o.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDal() { Id = 1, Name = "Mapped_FromMockMapper_ProductDalObj" });
            _repositoryMock.Setup(o => o.AddAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDal() { Id = 1, Name = "Added_FromMockRepository_ProductObj" });
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.AddAsync(new ProductDom());
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _repositoryMock.Verify(prm => prm.AddAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(pms => pms.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentNullException()
        {
            _mappingServiceMock.Setup(o => o.MapAsync(default(ProductDom)!, It.IsAny<CancellationToken>())).ReturnsAsync(default(ProductDal)!);
            _repositoryMock.Setup(o => o.AddAsync(default(ProductDal)!, It.IsAny<CancellationToken>())).Throws(new ArgumentNullException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.AddAsync(null!));
            _mappingServiceMock.Verify(pmsm => pmsm.MapAsync(default(ProductDom)!, It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(prm => prm.AddAsync(null!, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentException()
        {
            _mappingServiceMock.Setup(o => o.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.ProductDal_1);
            _repositoryMock.Setup(o => o.AddAsync(TestData.TestData.ProductDal_1, It.IsAny<CancellationToken>())).Throws(new ArgumentException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync(new ProductDom()));
            _mappingServiceMock.Verify(pmsm => pmsm.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(prm => prm.AddAsync(TestData.TestData.ProductDal_1, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void DeleteTest()
        {
            _mappingServiceMock.Setup(o => o.Map(It.IsAny<ProductDal>())).Returns(new ProductDom() { Id = 1 });
            _repositoryMock.Setup(o => o.Delete(It.IsAny<int>())).Returns(new ProductDal() { Id = 1, Name = "Deleted_FromMockRepository_ProductObj" });
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);


            var delRes = service.Delete(1);
            Assert.NotNull(delRes);
            Assert.NotEqual(0, delRes.Id);
            _repositoryMock.Verify(prm => prm.Delete(It.IsAny<int>()));
            _mappingServiceMock.Verify(pms => pms.Map(It.IsAny<ProductDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Thrown_ArgumentExceptionTest(int id)
        {
            _repositoryMock.Setup(o => o.Delete(id)).Throws(new ArgumentException());
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Delete(id));
            _repositoryMock.Verify(prm => prm.Delete(id));
        }
    }
}
