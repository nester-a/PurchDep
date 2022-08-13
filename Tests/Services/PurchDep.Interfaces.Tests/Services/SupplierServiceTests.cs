using Moq;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.Interfaces.Tests.Services.Fixtures;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

using SupplierDal = PurchDep.Dal.Entities.Supplier;
using SupplierDom = PurchDep.Domain.Supplier;

namespace PurchDep.Interfaces.Tests.Services
{
    [Collection("Service collection")]
    public class SupplierServiceTests
    {
        Mock<SupplierRepository> _repositoryMock;
        Mock<SupplierMappingService> _mappingServiceMock;
        private readonly ServiceFixture _fixture;

        public SupplierServiceTests(ServiceFixture fixture)
        {
            _repositoryMock = fixture.SupplierRepositoryMock;
            _mappingServiceMock = fixture.SupplierMappingServiceMock;
            _fixture = fixture;
        }

        [Fact]
        public void AddNewItem()
        {
            _repositoryMock.Setup(repo => repo.Add(It.IsAny<SupplierDal>())).Returns(new SupplierDal() { Id = 1, Name = "Added_FromMockRepository_DalObj" });
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<SupplierDom>())).Returns(new SupplierDal() { Id = 1, Name = "Mapped_FromMockMapper_DalObj" });

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.Add(new SupplierDom());
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _repositoryMock.Verify(repo => repo.Add(It.IsAny<SupplierDal>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<SupplierDom>()));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(default(SupplierDom)!)).Returns(default(SupplierDal)!);
            _repositoryMock.Setup(repo => repo.Add(default(SupplierDal)!)).Throws(new ArgumentNullException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentNullException>(() => service.Add(null!));
            _mappingServiceMock.Verify(mapper => mapper.Map(default(SupplierDom)!));
            _repositoryMock.Verify(repo => repo.Add(null!));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentException()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<SupplierDom>())).Returns(TestData.TestData.SupplierDal_1);
            _repositoryMock.Setup(repo => repo.Add(TestData.TestData.SupplierDal_1)).Throws(new ArgumentException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Add(new SupplierDom()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<SupplierDom>()));
            _repositoryMock.Verify(repo => repo.Add(TestData.TestData.SupplierDal_1));
        }

        [Fact]
        public async Task AddNewItemAsync()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDal() { Id = 1, Name = "Mapped_FromMockMapper_DalObj" });
            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDal() { Id = 1, Name = "Added_FromMockRepository_Obj" });
            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.AddAsync(new SupplierDom());
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentNullException()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(SupplierDom)!, It.IsAny<CancellationToken>())).ReturnsAsync(default(SupplierDal)!);
            _repositoryMock.Setup(repo => repo.AddAsync(default(SupplierDal)!, It.IsAny<CancellationToken>())).Throws(new ArgumentNullException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.AddAsync(null!));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(default(SupplierDom)!, It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.AddAsync(null!, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentException()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.SupplierDal_1);
            _repositoryMock.Setup(repo => repo.AddAsync(TestData.TestData.SupplierDal_1, It.IsAny<CancellationToken>())).Throws(new ArgumentException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync(new SupplierDom()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.AddAsync(TestData.TestData.SupplierDal_1, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void DeleteTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<SupplierDal>())).Returns(new SupplierDom() { Id = 1 });
            _repositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(new SupplierDal() { Id = 1, Name = "Deleted_FromMockRepository_Obj" });
            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);


            var delRes = service.Delete(1);
            Assert.NotNull(delRes);
            Assert.NotEqual(0, delRes.Id);
            _repositoryMock.Verify(repo => repo.Delete(It.IsAny<int>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<SupplierDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Thrown_ArgumentExceptionTest(int id)
        {
            _repositoryMock.Setup(repo => repo.Delete(id)).Throws(new ArgumentException());
            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Delete(id));
            _repositoryMock.Verify(repo => repo.Delete(id));
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDom() { Id = 1 });
            _repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDal() { Id = 1, Name = "Deleted_FromMockRepository_SupplierObj" });
            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);


            var delRes = await service.DeleteAsync(1);
            Assert.NotNull(delRes);
            Assert.NotEqual(0, delRes.Id);
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task DeleteAsync_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.DeleteAsync(id, It.IsAny<CancellationToken>())).Throws(new ArgumentException());
            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(id));
            _repositoryMock.Verify(prm => prm.DeleteAsync(id, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void GetTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<SupplierDal>())).Returns(TestData.TestData.SupplierDom_1);
            _repositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(TestData.TestData.SupplierDal_1);

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.Get(1);

            Assert.NotNull(res);
            Assert.Equal(TestData.TestData.SupplierDom_1.Id, res.Id);
            Assert.Equal(TestData.TestData.SupplierDom_1.Name, res.Name);
            _repositoryMock.Verify(repo => repo.Get(It.IsAny<int>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<SupplierDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Get_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.Get(id)).Throws(new ArgumentException());
            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Get(404));
            _repositoryMock.Verify(repo => repo.Get(id));
        }

        [Fact]
        public async Task GetAsyncTest()
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.SupplierDom_1);
            _repositoryMock.Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestData.TestData.SupplierDal_1);

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.GetAsync(1);

            Assert.NotNull(res);
            Assert.Equal(TestData.TestData.SupplierDom_1.Id, res.Id);
            Assert.Equal(TestData.TestData.SupplierDom_1.Name, res.Name);
            _repositoryMock.Verify(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Thrown_ArgumentException_Test(int id)
        {
            _repositoryMock.Setup(repo => repo.GetAsync(id, It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentException());
            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetAsync(404));
            _repositoryMock.Verify(repo => repo.GetAsync(id, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public void GetAllTest()
        {
            _repositoryMock.Setup(repo => repo.GetAll()).Returns(_fixture.SuppliersDal);
            _mappingServiceMock.Setup(mapper => mapper.MapRange(_fixture.SuppliersDal)).Returns(_fixture.SuppliersDom);

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = service.GetAll();

            Assert.NotEqual(0, res.Count);
            _repositoryMock.Verify(repo => repo.GetAll());
            _mappingServiceMock.Verify(mapper => mapper.MapRange(_fixture.SuppliersDal));
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            _repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.SuppliersDal);
            _mappingServiceMock.Setup(mapper => mapper.MapRangeAsync(_fixture.SuppliersDal, It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.SuppliersDom);

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            var res = await service.GetAllAsync();

            Assert.NotEqual(0, res.Count);
            _repositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapRangeAsync(_fixture.SuppliersDal, It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(1, "ItemToUpdate")]
        public void UpdateTest(int id, string newName)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<SupplierDom>())).Returns(new SupplierDal());
            _repositoryMock.Setup(repo => repo.Update(id, It.IsAny<SupplierDal>())).Returns(new SupplierDal() { Id = id, Name = newName });
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<SupplierDal>())).Returns(new SupplierDom() { Id = id, Name = newName });

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);
            var res = service.Update(id, new SupplierDom());

            Assert.Equal(id, res.Id);
            Assert.Equal(newName, res.Name);
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<SupplierDom>()));
            _repositoryMock.Verify(repo => repo.Update(id, It.IsAny<SupplierDal>()));
            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<SupplierDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(It.IsAny<SupplierDom>())).Returns(new SupplierDal());
            _repositoryMock.Setup(repo => repo.Update(id, It.IsAny<SupplierDal>())).Throws(new ArgumentException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentException>(() => service.Update(id, new SupplierDom()));

            _mappingServiceMock.Verify(mapper => mapper.Map(It.IsAny<SupplierDom>()));
            _repositoryMock.Verify(repo => repo.Update(id, It.IsAny<SupplierDal>()));
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentNullException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.Map(default(SupplierDom)!)).Returns(default(SupplierDal)!);
            _repositoryMock.Setup(repo => repo.Update(id, default(SupplierDal)!)).Throws(new ArgumentNullException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            Assert.Throws<ArgumentNullException>(() => service.Update(id, null!));

            _mappingServiceMock.Verify(mapper => mapper.Map(default(SupplierDom)!));
            _repositoryMock.Verify(repo => repo.Update(id, default(SupplierDal)!));
        }

        [Theory]
        [InlineData(1, "ItemToUpdateAsync")]
        public async Task UpdateAsyncTest(int id, string newName)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDal());
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDal() { Id = id, Name = newName });
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDom() { Id = id, Name = newName });

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);
            var res = await service.UpdateAsync(id, new SupplierDom());

            Assert.Equal(id, res.Id);
            Assert.Equal(newName, res.Name);
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>()));
            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>())).ReturnsAsync(new SupplierDal());
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.UpdateAsync(id, new SupplierDom()));

            _mappingServiceMock.Verify(mapper => mapper.MapAsync(It.IsAny<SupplierDom>(), It.IsAny<CancellationToken>()));
            _repositoryMock.Verify(repo => repo.UpdateAsync(id, It.IsAny<SupplierDal>(), It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentNullException_Test(int id)
        {
            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(SupplierDom)!, It.IsAny<CancellationToken>())).ReturnsAsync(default(SupplierDal)!);
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, default(SupplierDal)!, It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentNullException());

            var service = new SupplierService(_repositoryMock.Object, _mappingServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.UpdateAsync(id, null!));

            _mappingServiceMock.Setup(mapper => mapper.MapAsync(default(SupplierDom)!, It.IsAny<CancellationToken>()));
            _repositoryMock.Setup(repo => repo.UpdateAsync(id, default(SupplierDal)!, It.IsAny<CancellationToken>()));
        }
    }
}
