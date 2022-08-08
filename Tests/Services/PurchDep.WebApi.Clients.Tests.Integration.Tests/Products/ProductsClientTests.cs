using PurchDep.Dal;
using PurchDep.Domain;
using PurchDep.WebApi.Clients.Products;
using PurchDep.WebApi.Clients.Tests.Integration.Tests.Fixtures;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PurchDep.WebApi.Clients.Tests.Integration.Tests.Products
{
    [Collection("WebApi Client collection")]
    public class ProductsClientTests
    {
        private readonly HostFixture _fixture;
        ProductsClient _client;

        public ProductsClientTests(HostFixture fixture)
        {
            _fixture = fixture;
            _client = new ProductsClient(_fixture.HttpClient);
        }

        [Fact]
        public void GetAll_Test()
        {
            var res = _client.GetAll();
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Count);
        }

        [Fact]
        public async Task GetAllAsync_Test()
        {
            var res = await _client.GetAllAsync();
            Assert.NotNull(res);
            Assert.NotEqual(0, res.Count);
        }

        [Fact]
        public void Get_Test()
        {
            var res = _client.Get(TestData.Product1.Id);
            Assert.NotNull(res);
            Assert.True(res is Product);
            Assert.Equal(TestData.Product1.Id, res.Id);
            Assert.Equal(TestData.Product1.Name, res.Name);
            Assert.Equal(TestData.Product1.Price, res.Price);
        }

        [Theory]
        [InlineData(404)]
        public void Get_Returns_NotFound_Test(int id)
        {
            bool catched = false;
            Product res = default!;
            try
            {
                res = _client.Get(id);
            }
            catch (AggregateException ex)
            {
                catched = true;
                Assert.True(ex.InnerException is ArgumentException);
            }
            Assert.True(catched);
            Assert.Null(res);
        }

        [Fact]
        public async Task GetAsync_Test()
        {
            var res = await _client.GetAsync(TestData.Product1.Id);
            Assert.NotNull(res);
            Assert.True(res is Product);
            Assert.Equal(TestData.Product1.Id, res.Id);
            Assert.Equal(TestData.Product1.Name, res.Name);
            Assert.Equal(TestData.Product1.Price, res.Price);
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Returns_NotFound_Test(int id)
        {
            bool catched = false;
            Product res = default!;
            try
            {
                res = await _client.GetAsync(id);
                
            }
            catch (ArgumentException ex)
            {
                catched = true;
                Assert.True(ex is ArgumentException);
            }
            Assert.True(catched);
            Assert.Null(res);
        }

        [Fact]
        public void Add_Test()
        {
            var item = new Product() { Name = "Product_3", Price = 3.33m };

            var res = _client.Add(item);
            Assert.NotNull(res);
            Assert.True(res is Product);
            Assert.NotEqual(0, res.Id);
            Assert.Equal(item.Name, res.Name);
            Assert.Equal(item.Price, res.Price);
        }

        [Fact]
        public void Add_Null_Item_Returns_ArgumentNullException_Test()
        {
            bool catched = false;

            Product res = default;

            try
            {
                res = _client.Add(null);
            }
            catch (ArgumentNullException ex)
            {
                catched = true;
                Assert.True(ex is ArgumentNullException);
            }
            Assert.True(catched);
            Assert.Null(res);
        }

        [Fact]
        public void Add_Exsisted_Item_Test()
        {
            bool catched = false;
            Product item = new Product() { Id = 1, Name = "Product_1" };

            Product res = default;
            try
            {
                res = _client.Add(item);
            }
            catch (InvalidOperationException ex)
            {
                catched = true;
                Assert.True(ex is InvalidOperationException);
            }
            Assert.True(catched);
            Assert.Null(res);
        }

        [Fact]
        public async Task AddAsync_Test()
        {
            var item = new Product() { Name = "Product_3", Price = 3.33m };

            var res = await _client.AddAsync(item);
            Assert.NotNull(res);
            Assert.True(res is Product);
            Assert.NotEqual(0, res.Id);
            Assert.Equal(item.Name, res.Name);
            Assert.Equal(item.Price, res.Price);
        }

        [Fact]
        public async Task AddAsync_Null_Item_Returns_ArgumentNullException_Test()
        {
            bool catched = false;

            Product res = default;

            try
            {
                res = await _client.AddAsync(null);
            }
            catch (ArgumentNullException ex)
            {
                catched = true;
                Assert.True(ex is ArgumentNullException);
            }
            Assert.True(catched);
            Assert.Null(res);
        }

        [Fact]
        public async Task AddAsync_Exsisted_Item_Test()
        {
            bool catched = false;
            Product item = new Product() { Id = 1, Name = "Product_1" };

            Product res = default;
            try
            {
                res = await _client.AddAsync(item);
            }
            catch (InvalidOperationException ex)
            {
                catched = true;
                Assert.True(ex is InvalidOperationException);
            }
            Assert.True(catched);
            Assert.Null(res);
        }

        //UPDATE
        //DELETE
    }
}
