using Moq;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.Interfaces.Tests.Services.Fixtures;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using StockDal = PurchDep.Dal.Entities.Stock;
using StockDom = PurchDep.Domain.Stock;

namespace PurchDep.Interfaces.Tests.Services
{
    [Collection("Service collection")]
    public class StockServiceTests
    {
        Mock<StockRepository> _repositoryMock;
        Mock<StockMappingService> _mappingServiceMock;
        private readonly ServiceFixture _fixture;

        public StockServiceTests(ServiceFixture fixture)
        {
            _repositoryMock = fixture.StockRepositoryMock;
            _mappingServiceMock = fixture.StockMappingServiceMock;
            _fixture = fixture;
        }

        [Fact]
        public void AddNewItem()
        {
            _repositoryMock.Setup(repo => repo.Add(It.IsAny<StockDal>())).Returns(new StockDal() { Id = 1, Name = "Added_FromMockRepository_DalObj" });
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<StockDom>())).Returns(new StockDal() { Id = 1, Name = "Mapped_FromMockMapper_DalObj" });

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.Add(new StockDom());
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _repositoryMock.Verify(repo => repo.Add(It.IsAny<StockDal>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<StockDom>()));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(default(StockDom)!)).Returns(default(StockDal)!);
            _repositoryMock.Setup(repo => repo.Add(default(StockDal)!)).Throws(new ArgumentNullException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentNullException>(() => service.Add(null!));
            _mappingServiceMock.Verify(mapper => mapper.Map(default(StockDom)!));
            _repositoryMock.Verify(repo => repo.Add(null!));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentException()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<StockDom>())).Returns(TestData.TestData.StockDal_1);
            _repositoryMock.Setup(repo => repo.Add(TestData.TestData.StockDal_1)).Throws(new ArgumentException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Add(new StockDom()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<StockDom>()));
            _repositoryMock.Verify(repo => repo.Add(TestData.TestData.StockDal_1));
        }

        [Fact]
        public async Task AddNewItemAsync()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDal() { Id = 1, Name = "Mapped_FromMockMapper_DalObj" });
            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDal() { Id = 1, Name = "Added_FromMockRepository_Obj" });
            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.AddAsync(new StockDom());
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentNullException()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(StockDom)!, It.IsAny<CancellationToken>())).ReturnsAsync(default(StockDal)!);
            _repositoryMock.Setup(repo => repo.AddAsync(default(StockDal)!, It.IsAny<CancellationToken>())).Throws(new ArgumentNullException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.AddAsync(null!));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(default(StockDom)!, It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.AddAsync(null!, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentException()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.StockDal_1);
            _repositoryMock.Setup(repo => repo.AddAsync(TestData.TestData.StockDal_1, It.IsAny<CancellationToken>())).Throws(new ArgumentException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync(new StockDom()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.AddAsync(TestData.TestData.StockDal_1, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void DeleteTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<StockDal>())).Returns(new StockDom() { Id = 1 });
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(new StockDal() { Id = 1, Name = "Deleted_FromMockRepository_Obj" });
            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);


            var delRes = service.Delete(1);
            Assert.NotNull(delRes);
            Assert.NotEqual(0, delRes.Id);
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<StockDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Thrown_ArgumentExceptionTest(int id)
        {
            _repositoryMock.Setup(repo => repo.Delete(id)).Throws(new ArgumentException());
            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Delete(id));
            _repositoryMock.Verify(repo => repo.Delete(id));
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDom() { Id = 1 });
            _repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDal() { Id = 1, Name = "Deleted_FromMockRepository_StockObj" });
            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);


            var delRes = await service.DeleteAsync(1);
            Assert.NotNull(delRes);
            Assert.NotEqual(0, delRes.Id);
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task DeleteAsync_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.DeleteAsync(id, It.IsAny<CancellationToken>())).Throws(new ArgumentException());
            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(id));
            _repositoryMock.Verify(prm => prm.DeleteAsync(id, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void GetTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<StockDal>())).Returns(TestData.TestData.StockDom_1);
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(TestData.TestData.StockDal_1);

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.Get(1);

            Assert.NotNull(res);
            Assert.Equal(TestData.TestData.StockDom_1.Id, res.Id);
            Assert.Equal(TestData.TestData.StockDom_1.Name, res.Name);
            _repositoryMock.Verify(repo => repo.Get(It.IsAny<int>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<StockDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Get_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.Get(id)).Throws(new ArgumentException());
            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Get(404));
            _repositoryMock.Verify(repo => repo.Get(id));
        }

        [Fact]
        public async Task GetAsyncTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.StockDom_1);
            _repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.StockDal_1);

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.GetAsync(1);

            Assert.NotNull(res);
            Assert.Equal(TestData.TestData.StockDom_1.Id, res.Id);
            Assert.Equal(TestData.TestData.StockDom_1.Name, res.Name);
            _repositoryMock.Verify(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.GetAsync(id, It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentException());
            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetAsync(404));
            _repositoryMock.Verify(repo => repo.GetAsync(id, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void GetAllTest()
        {
            _repositoryMock.Setup(repo => repo.GetAll()).Returns(_fixture.StocksDal);
            _mappingServiceMock.Setup(mapper => mapper.MapRange(_fixture.StocksDal)).Returns(_fixture.StocksDom);

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.GetAll();

            Assert.NotEqual(0, res.Count);
            _repositoryMock.Verify(repo => repo.GetAll());
            _mappingServiceMock.Verify(mapper => mapper.MapRange(_fixture.StocksDal));
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.StocksDal);
            _mappingServiceMock.Setup(mapper => mapper.MapRangeAsync(_fixture.StocksDal, It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.StocksDom);

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.GetAllAsync();

            Assert.NotEqual(0, res.Count);
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapRangeAsync(_fixture.StocksDal, It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(1, "ItemToUpdate")]
        public void UpdateTest(int id, string newName)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<StockDom>())).Returns(new StockDal());
            _repositoryMock.Setup(repo => repo.Update(id, It.IsAny<StockDal>())).Returns(new StockDal() { Id = id, Name = newName });
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<StockDal>())).Returns(new StockDom() { Id = id, Name = newName });

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);
            var res = service.Update(id, new StockDom());

            Assert.Equal(id, res.Id);
            Assert.Equal(newName, res.Name);
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<StockDom>()));
            _repositoryMock.Verify(repo => repo.Update(id, It.IsAny<StockDal>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<StockDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<StockDom>())).Returns(new StockDal());
            _repositoryMock.Setup(repo => repo.Update(id, It.IsAny<StockDal>())).Throws(new ArgumentException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Update(id, new StockDom()));

            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<StockDom>()));
            _repositoryMock.Verify(repo => repo.Update(id, It.IsAny<StockDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentNullException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(default(StockDom)!)).Returns(default(StockDal)!);
            _repositoryMock.Setup(repo => repo.Update(id, default(StockDal)!)).Throws(new ArgumentNullException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentNullException>(() => service.Update(id, null!));

            _mappingServiceMock.Verify(mapper => mapper.Map(default(StockDom)!));
            _repositoryMock.Verify(repo => repo.Update(id, default(StockDal)!));
        }

        [Theory]
        [InlineData(1, "ItemToUpdateAsync")]
        public async Task UpdateAsyncTest(int id, string newName)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDal());
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<StockDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDal() { Id = id, Name = newName });
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDom() { Id = id, Name = newName });

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);
            var res = await service.UpdateAsync(id, new StockDom());

            Assert.Equal(id, res.Id);
            Assert.Equal(newName, res.Name);
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<StockDal>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<StockDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new StockDal());
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<StockDal>(), It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.UpdateAsync(id, new StockDom()));

            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<StockDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<StockDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentNullException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(StockDom)!, It.IsAny<CancellationToken>())).ReturnsAsync(default(StockDal)!);
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, default(StockDal)!, It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentNullException());

            var service = new StockService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.UpdateAsync(id, null!));

            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(StockDom)!, It.IsAny<CancellationToken>()));
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, default(StockDal)!, It.IsAny<CancellationToken>()));
        }

    }
}
