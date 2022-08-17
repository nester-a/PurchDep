using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Tests.Repositories.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PurchDep.Interfaces.Tests.Repositories
{
    [Collection("Repo Database collection")]
    public class SupplierRepositoryTests
    {
        DbFixture _fixture;
        Repository<Supplier> _repo;
        public SupplierRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _repo = new SupplierRepository(fixture.Db);
        }


        [Fact]
        public void AddNewItem()
        {
            var item = TestData.TestData.SupplierDal_ForAdding;
            var res = _repo.Add(item);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.True(_fixture.Db.Suppliers.Contains(res));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repo.Add(null!));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentException()
        {
            
            Assert.True(_fixture.Db.Suppliers.Contains(TestData.TestData.SupplierDal_1));
            Assert.Throws<ArgumentException>(() => _repo.Add(TestData.TestData.SupplierDal_1));
        }

        [Fact]
        public async Task AddNewItemAsync()
        {
            var item = TestData.TestData.SupplierDal_ForAddingAsync;
            var res = await _repo.AddAsync(item);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.True(_fixture.Db.Suppliers.Contains(res));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _repo.AddAsync(null!));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _repo.AddAsync(TestData.TestData.SupplierDal_1));
        }

        [Fact]
        public void DeleteTest()
        {
            var res = _repo.Add(TestData.TestData.SupplierDal_ForDeleting);
            Assert.True(_fixture.Db.Suppliers.Contains(res));
            Assert.NotEqual(0, res.Id);

            var delRes = _repo.Delete(res.Id);
            Assert.Equal(res.Id, delRes.Id);
            Assert.False(_fixture.Db.Suppliers.Contains(delRes));
            Assert.False(_fixture.Db.Suppliers.Contains(res));
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Thrown_ArgumentExceptionTest(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));
            Assert.Throws<ArgumentException>(() => _repo.Delete(id));
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            var res = await _repo.AddAsync(TestData.TestData.SupplierDal_ForDeletingAsync);
            Assert.True(_fixture.Db.Suppliers.Contains(res));
            Assert.NotEqual(0, res.Id);

            var delRes = await _repo.DeleteAsync(res.Id);
            Assert.Equal(res.Id, delRes.Id);
            Assert.False(_fixture.Db.Suppliers.Contains(delRes));
            Assert.False(_fixture.Db.Suppliers.Contains(res));
        }

        [Theory]
        [InlineData(404)]
        public async Task DeleteAsync_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Suppliers.FirstOrDefault(x => x.Id == id));
            await Assert.ThrowsAsync<ArgumentException>(async () => await _repo.DeleteAsync(id));
        }

        [Fact]
        public void GetTest()
        {
            var item = TestData.TestData.SupplierDal_1;
            Assert.NotEqual(0, item.Id);
            var res = _repo.Get(item.Id);

            Assert.NotNull(res);
            Assert.Equal(item.Id, res.Id);
            Assert.Equal(item.Name, res.Name);
        }

        [Theory]
        [InlineData(404)]
        public void Get_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Suppliers.FirstOrDefault(x => x.Id == id));
            Assert.Throws<ArgumentException>(() => _repo.Get(id));
        }

        [Fact]
        public async Task GetAsyncTest()
        {
            var item = TestData.TestData.SupplierDal_1;
            Assert.NotEqual(0, item.Id);
            var res = await _repo.GetAsync(item.Id);

            Assert.NotNull(res);
            Assert.Equal(item.Id, res.Id);
            Assert.Equal(item.Name, res.Name);
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Suppliers.FirstOrDefault(x => x.Id == id));
            await Assert.ThrowsAsync<ArgumentException>(async () => await _repo.GetAsync(id));
        }

        [Fact]
        public void GetAllTest()
        {
            var res = _repo.GetAll();

            Assert.NotEqual(0, res.Count);
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(TestData.TestData.SupplierDal_1.Id)));
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            var res = await _repo.GetAllAsync();

            Assert.NotEqual(0, res.Count);
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(TestData.TestData.SupplierDal_1.Id)));
        }

        [Theory]
        [InlineData("SupplierToUpdate")]
        public void UpdateTest(string newName)
        {
            var item = TestData.TestData.SupplierDal_ForUpdating;
            var supplierToUpdate = new Supplier { Name = newName };
            
            _repo.Add(item);
            var res = _repo.Update(item.Id, supplierToUpdate);

            Assert.Equal(item.Id, res.Id);
            Assert.Equal(item.Name, res.Name);
            Assert.Equal(item.Name, newName);
        }

        [Theory]
        [InlineData(404, "404")]
        public void Update_Thrown_ArgumentException_Test(int id, string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            Assert.Throws<ArgumentException>(() => _repo.Update(id, supplierToUpdate));
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentNullException_Test(int id)
        {
            Assert.Throws<ArgumentNullException>(() => _repo.Update(id, null!));
        }

        [Theory]
        [InlineData("SupplierToUpdateAsync")]
        public async Task UpdateAsyncTest(string newName)
        {
            var item = TestData.TestData.SupplierDal_ForUpdatingAsync;
            var supplierToUpdate = new Supplier { Name = newName };

            await _repo.AddAsync(item);
            var res = await _repo.UpdateAsync(item.Id, supplierToUpdate);

            Assert.Equal(item.Id, res.Id);
            Assert.Equal(item.Name, res.Name);
            Assert.Equal(item.Name, newName);
        }

        [Theory]
        [InlineData(404, "404")]
        public async Task UpdateAsync_Thrown_ArgumentException_Test(int id, string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            await Assert.ThrowsAsync<ArgumentException>(async () => await _repo.UpdateAsync(id, supplierToUpdate));
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentNullException_Test(int id)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _repo.UpdateAsync(id, null!));
        }
    }
}
