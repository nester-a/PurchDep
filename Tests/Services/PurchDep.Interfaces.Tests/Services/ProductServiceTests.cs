using Moq;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.Interfaces.Tests.Services.Fixtures;
using System;
using System.Collections.Generic;
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
        private readonly ServiceFixture _fixture;

        public ProductServiceTests(ServiceFixture fixture)
        {
            _repositoryMock = fixture.ProductRepositoryMock;
            _mappingServiceMock = fixture.ProductMappingServiceMock;
            _fixture = fixture;
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

        [Fact]
        public async Task DeleteAsyncTest()
        {
            _mappingServiceMock.Setup(o => o.MapAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDom() { Id = 1 });
            _repositoryMock.Setup(o => o.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDal() { Id = 1, Name = "Deleted_FromMockRepository_ProductObj" });
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);


            var delRes = await service.DeleteAsync(1);
            Assert.NotNull(delRes);
            Assert.NotEqual(0, delRes.Id);
            _repositoryMock.Verify(prm => prm.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(pms => pms.MapAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task DeleteAsync_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(o => o.DeleteAsync(id, It.IsAny<CancellationToken>())).Throws(new ArgumentException());
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(id));
            _repositoryMock.Verify(prm => prm.DeleteAsync(id, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void GetTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<ProductDal>())).Returns(TestData.TestData.ProductDom_1);
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(TestData.TestData.ProductDal_1);

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.Get(1);

            Assert.NotNull(res);
            Assert.Equal(TestData.TestData.ProductDom_1.Id, res.Id);
            Assert.Equal(TestData.TestData.ProductDom_1.Name, res.Name);
            _repositoryMock.Verify(repo => repo.Get(It.IsAny<int>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<ProductDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Get_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.Get(id)).Throws(new ArgumentException());
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Get(404));
            _repositoryMock.Verify(repo => repo.Get(id));
        }

        [Fact]
        public async Task GetAsyncTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.ProductDom_1);
            _repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.ProductDal_1);

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.GetAsync(1);

            Assert.NotNull(res);
            Assert.Equal(TestData.TestData.ProductDom_1.Id, res.Id);
            Assert.Equal(TestData.TestData.ProductDom_1.Name, res.Name);
            _repositoryMock.Verify(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.GetAsync(id, It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentException());
            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetAsync(404));
            _repositoryMock.Verify(repo => repo.GetAsync(id, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void GetAllTest()
        {
            _repositoryMock.Setup(repo => repo.GetAll()).Returns(_fixture.ProductsDal);
            _mappingServiceMock.Setup(mapper => mapper.MapRange(_fixture.ProductsDal)).Returns(_fixture.ProductsDom);

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.GetAll();

            Assert.NotEqual(0, res.Count);
            _repositoryMock.Verify(repo => repo.GetAll());
            _mappingServiceMock.Verify(mapper => mapper.MapRange(_fixture.ProductsDal));
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.ProductsDal);
            _mappingServiceMock.Setup(mapper => mapper.MapRangeAsync(_fixture.ProductsDal, It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.ProductsDom);

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.GetAllAsync();

            Assert.NotEqual(0, res.Count);
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapRangeAsync(_fixture.ProductsDal, It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(1, "ItemToUpdate")]
        public void UpdateTest(int id, string newName)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<ProductDom>())).Returns(new ProductDal());
            _repositoryMock.Setup(repo => repo.Update(id, It.IsAny<ProductDal>())).Returns(new ProductDal() { Id = id, Name = newName });
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<ProductDal>())).Returns(new ProductDom() { Id = id, Name = newName });

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);
            var res = service.Update(id, new ProductDom());

            Assert.Equal(id, res.Id);
            Assert.Equal(newName, res.Name);
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<ProductDom>()));
            _repositoryMock.Verify(repo => repo.Update(id, It.IsAny<ProductDal>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<ProductDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<ProductDom>())).Returns(new ProductDal());
            _repositoryMock.Setup(repo => repo.Update(id, It.IsAny<ProductDal>())).Throws(new ArgumentException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(()=> service.Update(id, new ProductDom()));

            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<ProductDom>()));
            _repositoryMock.Verify(repo => repo.Update(id, It.IsAny<ProductDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentNullException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(default(ProductDom)!)).Returns(default(ProductDal)!);
            _repositoryMock.Setup(repo => repo.Update(id, default(ProductDal)!)).Throws(new ArgumentNullException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentNullException>(() => service.Update(id, null!));

            _mappingServiceMock.Verify(mapper => mapper.Map(default(ProductDom)!));
            _repositoryMock.Verify(repo => repo.Update(id, default(ProductDal)!));
        }

        [Theory]
        [InlineData(1, "ItemToUpdateAsync")]
        public async Task UpdateAsyncTest(int id, string newName)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDal());
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<ProductDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDal() { Id = id, Name = newName });
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDom() { Id = id, Name = newName });

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);
            var res = await service.UpdateAsync(id, new ProductDom());

            Assert.Equal(id, res.Id);
            Assert.Equal(newName, res.Name);
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<ProductDal>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<ProductDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ProductDal());
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<ProductDal>(), It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.UpdateAsync(id, new ProductDom()));

            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<ProductDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<ProductDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentNullException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(ProductDom)!, It.IsAny<CancellationToken>())).ReturnsAsync(default(ProductDal)!);
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, default(ProductDal)!, It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentNullException());

            var service = new ProductService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.UpdateAsync(id, null!));

            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(ProductDom)!, It.IsAny<CancellationToken>()));
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, default(ProductDal)!, It.IsAny<CancellationToken>()));
        }
    }
}
