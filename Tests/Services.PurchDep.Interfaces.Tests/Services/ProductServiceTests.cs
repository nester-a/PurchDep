using PurchDep.Interfaces.Base.Services;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using Services.PurchDep.Interfaces.Tests.Data;
using Services.PurchDep.Interfaces.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using PurchDep.Domain;
using System.Threading.Tasks;
using Xunit;

using ProductDal = PurchDep.Dal.Entities.Product;
using ProductDom = PurchDep.Domain.Base.IProduct;

namespace Services.PurchDep.Interfaces.Tests.Services
{
    [Collection("Service Database collection")]
    public class ProductServiceTests
    {
        ServiceDbFixture _fixture;
        Service<ProductDal, ProductDom> _service;
        ProductMappingService _mapper;
        public ProductServiceTests(ServiceDbFixture fixture)
        {
            _fixture = fixture;
            var repo = new ProductRepository(_fixture.Db);
            _mapper = new ProductMappingService();
            _service = new ProductService(repo, _mapper);
        }


        [Fact]
        public void AddNewItem()
        {
            var res = _service.Add(ServicesTestData.Product3);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.NotNull(_fixture.Db.Products.FirstOrDefault(x => x.Id == res.Id));
        }

        [Fact]
        public void AddNewItem_Thrown_ArgumentNullException()
        {
            bool cathced = false;
            try
            {
                _service.Add(null);
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
            Assert.True(_fixture.Db.Products.Contains(ServicesTestData.Product1));
            bool cathced = false;
            try
            {
                _service.Add(_mapper.Map(ServicesTestData.Product1));
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
            var res = await _service.AddAsync(ServicesTestData.Product4);

            Assert.NotNull(res);
            Assert.NotEqual(0, res.Id);
            Assert.NotNull(_fixture.Db.Products.FirstOrDefault(x => x.Id == res.Id));
        }

        [Fact]
        public async Task AddNewItemAsync_Thrown_ArgumentNullException()
        {
            bool cathced = false;
            try
            {
                await _service.AddAsync(null);
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
            Assert.True(_fixture.Db.Products.Contains(ServicesTestData.Product2));
            bool cathced = false;
            try
            {
                await _service.AddAsync(_mapper.Map(ServicesTestData.Product2));
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
            var res = _service.Add(ServicesTestData.Product5);
            Assert.NotNull(_fixture.Db.Products.FirstOrDefault(x => x.Id == res.Id));
            Assert.NotEqual(0, res.Id);

            var delRes = _service.Delete(res.Id);
            Assert.Equal(res.Id, delRes.Id);
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == delRes.Id));
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Thrown_ArgumentExceptionTest(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));
            bool cathced = false;
            try
            {
                _service.Delete(id);
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
            var res = await _service.AddAsync(ServicesTestData.Product6);
            Assert.NotNull(_fixture.Db.Products.FirstOrDefault(x => x.Id == res.Id));
            Assert.NotEqual(0, res.Id);

            var delRes = await _service.DeleteAsync(res.Id);
            Assert.Equal(res.Id, delRes.Id);
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == delRes.Id));
        }

        [Theory]
        [InlineData(404)]
        public async Task DeleteAsync_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));
            bool cathced = false;
            try
            {
                await _service.DeleteAsync(id);
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
            Assert.NotEqual(0, ServicesTestData.Product1.Id);
            var res = _service.Get(ServicesTestData.Product1.Id);

            Assert.NotNull(res);
            Assert.Equal(ServicesTestData.Product1.Id, res.Id);
            Assert.Equal(ServicesTestData.Product1.Name, res.Name);
            Assert.Equal(ServicesTestData.Product1.Price, res.Price);
        }

        [Theory]
        [InlineData(404)]
        public void Get_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));

            bool catched = false;
            try
            {
                _service.Get(404);
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
            Assert.NotEqual(0, ServicesTestData.Product2.Id);
            var res = await _service.GetAsync(ServicesTestData.Product2.Id);

            Assert.NotNull(res);
            Assert.Equal(ServicesTestData.Product2.Id, res.Id);
            Assert.Equal(ServicesTestData.Product2.Name, res.Name);
            Assert.Equal(ServicesTestData.Product2.Price, res.Price);
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Thrown_ArgumentException_Test(int id)
        {
            Assert.Null(_fixture.Db.Products.FirstOrDefault(x => x.Id == id));

            bool catched = false;
            try
            {
                await _service.GetAsync(404);
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
            var res = _service.GetAll();

            Assert.NotEqual(0, res.Count);
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(ServicesTestData.Product1.Id)));
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(ServicesTestData.Product2.Id)));
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            var res = await _service.GetAllAsync();

            Assert.NotEqual(0, res.Count);
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(ServicesTestData.Product1.Id)));
            Assert.NotNull(res.FirstOrDefault(x => x.Id.Equals(ServicesTestData.Product2.Id)));
        }

        [Theory]
        [InlineData("ProductToUpdate")]
        public void UpdateTest(string newName)
        {
            var productToUpdate = new Product { Name = newName };
            var res = _service.Update(ServicesTestData.Product1.Id, productToUpdate);

            Assert.Equal(ServicesTestData.Product1.Id, res.Id);
            Assert.Equal(ServicesTestData.Product1.Name, res.Name);
            Assert.Equal(ServicesTestData.Product1.Name, newName);
            Assert.Equal(ServicesTestData.Product1.Price, res.Price);
        }

        [Theory]
        [InlineData(404, "404")]
        public void Update_Thrown_ArgumentException_Test(int id, string newName)
        {
            var productToUpdate = new Product { Name = newName };
            bool catched = false;

            try
            {
                _service.Update(id, productToUpdate);
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
                _service.Update(id, null);
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
            var res = await _service.UpdateAsync(RepoTestData.Product2.Id, productToUpdate);

            Assert.Equal(ServicesTestData.Product2.Id, res.Id);
            Assert.Equal(ServicesTestData.Product2.Name, res.Name);
            Assert.Equal(ServicesTestData.Product2.Name, newName);
            Assert.Equal(ServicesTestData.Product2.Price, res.Price);
        }

        [Theory]
        [InlineData(404, "404")]
        public async Task UpdateAsync_Thrown_ArgumentException_Test(int id, string newName)
        {
            var productToUpdate = new Product { Name = newName };
            bool catched = false;

            try
            {
                await _service.UpdateAsync(id, productToUpdate);
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
                await _service.UpdateAsync(id, null);
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
