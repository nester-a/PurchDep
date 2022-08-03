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
    public class ProductRepositoryTests
    {
        DbFixture _fixture;
        Repository<Product> _repo;
        public ProductRepositoryTests(DbFixture fixture)
        {
            _fixture = fixture;
            _repo = new ProductRepository(fixture.Db);
        }

        [Fact]
        public void AddNewItem()
        {
            var res = _repo.Add(RepoTestData.Product3);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.True(_fixture.Db.Products.Contains(res));
        }
        
        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            bool cathced = false;
            try
            {
                _repo.Add(null);
            }
            catch(ArgumentNullException e)
            {
                cathced = true;
                Assert.True(e is not null);
            }
            Assert.True(cathced);
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentException()
        {
            Assert.True(_fixture.Db.Products.Contains(RepoTestData.Product1));
            bool cathced = false;
            try
            {
                _repo.Add(RepoTestData.Product1);
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
            var res = await _repo.AddAsync(RepoTestData.Product4);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.True(_fixture.Db.Products.Contains(res));
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
            Assert.True(_fixture.Db.Products.Contains(RepoTestData.Product2));
            bool cathced = false;
            try
            {
                await _repo.AddAsync(RepoTestData.Product2);
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
            var res = _repo.Add(RepoTestData.Product5);
            Assert.True(_fixture.Db.Products.Contains(res));
            Assert.NotEqual(0, res.Id);

            var delRes = _repo.Delete(res.Id);
            Assert.Equal(res.Id, delRes.Id);
            Assert.False(_fixture.Db.Products.Contains(delRes));
            Assert.False(_fixture.Db.Products.Contains(res));
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
            var res = await _repo.AddAsync(RepoTestData.Product6);
            Assert.True(_fixture.Db.Products.Contains(res));
            Assert.NotEqual(0, res.Id);

            var delRes = await _repo.DeleteAsync(res.Id);
            Assert.Equal(res.Id, delRes.Id);
            Assert.False(_fixture.Db.Products.Contains(delRes));
            Assert.False(_fixture.Db.Products.Contains(res));
        }

        [Theory]
        [InlineData(404)]
        public async Task DeleteAsync_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));
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
            Assert.NotEqual(0, RepoTestData.Product1.Id);
            var res = _repo.Get(RepoTestData.Product1.Id);

            Assert.NotNull(res);
            Assert.Equal(RepoTestData.Product1.Id, res.Id);
            Assert.Equal(RepoTestData.Product1.Name, res.Name);
            Assert.Equal(RepoTestData.Product1.Price, res.Price);
        }

        [Theory]
        [InlineData(404)]
        public void Get_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));

            bool catched = false;
            try
            {
                _repo.Get(404);
            }
            catch(ArgumentException e)
            {
                catched = true;
                Assert.NotNull(e);
            }

            Assert.True(catched);
        }

        [Fact]
        public async Task GetAsyncTest()
        {
            Assert.NotEqual(0, RepoTestData.Product2.Id);
            var res = await _repo.GetAsync(RepoTestData.Product2.Id);

            Assert.NotNull(res);
            Assert.Equal(RepoTestData.Product2.Id, res.Id);
            Assert.Equal(RepoTestData.Product2.Name, res.Name);
            Assert.Equal(RepoTestData.Product2.Price, res.Price);
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));

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
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Product1.Id)));
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Product2.Id)));
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            var res = await _repo.GetAllAsync();

            Assert.NotEqual(0, res.Count);
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Product1.Id)));
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(RepoTestData.Product2.Id)));
        }

        [Theory]
        [InlineData("ProductToUpdate")]
        public void UpdateTest(string newName)
        {
            var productToUpdate = new Product { Name = newName };
            var res = _repo.Update(RepoTestData.Product1.Id, productToUpdate);

            Assert.Equal(RepoTestData.Product1.Id, res.Id);
            Assert.Equal(RepoTestData.Product1.Name, res.Name);
            Assert.Equal(RepoTestData.Product1.Name, newName);
            Assert.Equal(RepoTestData.Product1.Price, res.Price);
        }

        [Theory]
        [InlineData(404, "404")]
        public void Update_Thrown_ArgumentException_Test(int id, string newName)
        {
            var productToUpdate = new Product { Name = newName };
            bool catched = false;

            try
            {
                _repo.Update(id, productToUpdate);
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
        [InlineData("ProductToUpdateAsync")]
        public async Task UpdateAsyncTest(string newName)
        {
            var productToUpdate = new Product { Name = newName };
            var res = await _repo.UpdateAsync(RepoTestData.Product2.Id, productToUpdate);

            Assert.Equal(RepoTestData.Product2.Id, res.Id);
            Assert.Equal(RepoTestData.Product2.Name, res.Name);
            Assert.Equal(RepoTestData.Product2.Name, newName);
            Assert.Equal(RepoTestData.Product2.Price, res.Price);
        }

        [Theory]
        [InlineData(404, "404")]
        public async Task UpdateAsync_Thrown_ArgumentException_Test(int id, string newName)
        {
            var productToUpdate = new Product { Name = newName };
            bool catched = false;

            try
            {
                await _repo.UpdateAsync(id, productToUpdate);
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
