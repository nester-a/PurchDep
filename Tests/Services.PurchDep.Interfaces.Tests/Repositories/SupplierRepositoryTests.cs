using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Repositories;
using Services.PurchDep.Interfaces.Tests.Data;
using Services.PurchDep.Interfaces.Tests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Services.PurchDep.Interfaces.Tests.Repositories
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
            var res = _repo.Add(RepoTestData.Supplier3);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.True(_fixture.Db.Suppliers.Contains(res));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            bool cathced = false;
            try
            {
                _repo.Add(null);
            }
            catch (ArgumentNullException e)
            {
                cathced = true;
                Assert.True(e is not null);
            }
            Assert.True(cathced);
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentException()
        {
            Assert.True(_fixture.Db.Suppliers.Contains(RepoTestData.Supplier1));
            bool cathced = false;
            try
            {
                _repo.Add(RepoTestData.Supplier1);
            }
            catch (ArgumentException e)
            {
                cathced = true;
                Assert.True(e is not null);
            }
            Assert.True(cathced);
        }

        [Fact]
        public async Task AddNewItemAsync()
        {
            var res = await _repo.AddAsync(RepoTestData.Supplier4);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.True(_fixture.Db.Suppliers.Contains(res));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentNullException()
        {
            bool cathced = false;
            try
            {
                await _repo.AddAsync(null);
            }
            catch (ArgumentNullException e)
            {
                cathced = true;
                Assert.True(e is not null);
            }
            Assert.True(cathced);
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentException()
        {
            Assert.True(_fixture.Db.Suppliers.Contains(RepoTestData.Supplier2));
            bool cathced = false;
            try
            {
                await _repo.AddAsync(RepoTestData.Supplier2);
            }
            catch (ArgumentException e)
            {
                cathced = true;
                Assert.True(e is not null);
            }
            Assert.True(cathced);
        }

        [Fact]
        public void DeleteTest()
        {
            var res = _repo.Add(RepoTestData.Supplier5);
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
            bool cathced = false;
            try
            {
                _repo.Delete(id);
            }
            catch (ArgumentException e)
            {
                cathced = true;
                Assert.True(e is not null);
            }
            Assert.True(cathced);
        }

        [Fact]
        public async Task DeleteAsyncTest()
        {
            var res = await _repo.AddAsync(RepoTestData.Supplier6);
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
            bool cathced = false;
            try
            {
                await _repo.DeleteAsync(id);
            }
            catch (ArgumentException e)
            {
                cathced = true;
                Assert.True(e is not null);
            }
            Assert.True(cathced);
        }

        [Fact]
        public void GetTest()
        {
            Assert.NotEqual(0, RepoTestData.Supplier1.Id);
            var res = _repo.Get(RepoTestData.Supplier1.Id);

            Assert.NotNull(res);
            Assert.Equal(RepoTestData.Supplier1.Id, res.Id);
            Assert.Equal(RepoTestData.Supplier1.Name, res.Name);
        }

        [Theory]
        [InlineData(404)]
        public void Get_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Suppliers.FirstOrDefault(x => x.Id == id));

            bool catched = false;
            try
            {
                _repo.Get(404);
            }
            catch (ArgumentException e)
            {
                catched = true;
                Assert.NotNull(e);
            }

            Assert.True(catched);
        }

        [Fact]
        public async Task GetAsyncTest()
        {
            Assert.NotEqual(0, RepoTestData.Supplier2.Id);
            var res = await _repo.GetAsync(RepoTestData.Supplier2.Id);

            Assert.NotNull(res);
            Assert.Equal(RepoTestData.Supplier2.Id, res.Id);
            Assert.Equal(RepoTestData.Supplier2.Name, res.Name);
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Suppliers.FirstOrDefault(x => x.Id == id));

            bool catched = false;
            try
            {
                await _repo.GetAsync(404);
            }
            catch (ArgumentException e)
            {
                catched = true;
                Assert.NotNull(e);
            }

            Assert.True(catched);
        }

        [Fact]
        public void GetAllTest()
        {
            var res = _repo.GetAll();

            Assert.NotEqual(0, res.Count);
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Supplier1.Id)));
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Supplier2.Id)));
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            var res = await _repo.GetAllAsync();

            Assert.NotEqual(0, res.Count);
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Supplier1.Id)));
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Supplier2.Id)));
        }

        [Theory]
        [InlineData("SupplierToUpdate")]
        public void UpdateTest(string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            var res = _repo.Update(RepoTestData.Supplier1.Id, supplierToUpdate);

            Assert.Equal(RepoTestData.Supplier1.Id, res.Id);
            Assert.Equal(RepoTestData.Supplier1.Name, res.Name);
            Assert.Equal(RepoTestData.Supplier1.Name, newName);
        }

        [Theory]
        [InlineData(404, "404")]
        public void Update_Thrown_ArgumentException_Test(int id, string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            bool catched = false;

            try
            {
                _repo.Update(id, supplierToUpdate);
            }
            catch (ArgumentException e)
            {
                catched = true;
                Assert.NotNull(e);
            }
            Assert.True(catched);
        }

        [Theory]
        [InlineData(404)]
        public void Update_Thrown_ArgumentNullException_Test(int id)
        {
            bool catched = false;

            try
            {
                _repo.Update(id, null);
            }
            catch (ArgumentNullException e)
            {
                catched = true;
                Assert.NotNull(e);
            }
            Assert.True(catched);
        }

        [Theory]
        [InlineData("SupplierToUpdateAsync")]
        public async Task UpdateAsyncTest(string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            var res = await _repo.UpdateAsync(RepoTestData.Supplier2.Id, supplierToUpdate);

            Assert.Equal(RepoTestData.Supplier2.Id, res.Id);
            Assert.Equal(RepoTestData.Supplier2.Name, res.Name);
            Assert.Equal(RepoTestData.Supplier2.Name, newName);
        }

        [Theory]
        [InlineData(404, "404")]
        public async Task UpdateAsync_Thrown_ArgumentException_Test(int id, string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            bool catched = false;

            try
            {
                await _repo.UpdateAsync(id, supplierToUpdate);
            }
            catch (ArgumentException e)
            {
                catched = true;
                Assert.NotNull(e);
            }
            Assert.True(catched);
        }

        [Theory]
        [InlineData(404)]
        public async Task UpdateAsync_Thrown_ArgumentNullException_Test(int id)
        {
            bool catched = false;

            try
            {
                await _repo.UpdateAsync(id, null);
            }
            catch (ArgumentNullException e)
            {
                catched = true;
                Assert.NotNull(e);
            }
            Assert.True(catched);
        }
    }
}
