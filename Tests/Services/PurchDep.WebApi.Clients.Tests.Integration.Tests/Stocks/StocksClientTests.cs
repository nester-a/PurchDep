using PurchDep.Dal;
using PurchDep.Domain;
using PurchDep.WebApi.Clients.Stocks;
using PurchDep.WebApi.Clients.Tests.Integration.Tests.Fixtures;
using System;
using System.Threading.Tasks;
using Xunit;
namespace PurchDep.WebApi.Clients.Tests.Integration.Tests.Stocks
{
    [Collection("WebApi Client collection")]
    public class StocksClientTests
    {
        private readonly HostFixture _fixture;
        StocksClient _client;

        public StocksClientTests(HostFixture fixture)
        {
            _fixture = fixture;
            _client = new StocksClient(_fixture.HttpClient);
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
            var res = _client.Get(TestData.Stock1.Id);
            Assert.NotNull(res);
            Assert.True(res is Stock);
            Assert.Equal(TestData.Stock1.Id, res.Id);
            Assert.Equal(TestData.Stock1.Name, res.Name);
        }

        [Theory]
        [InlineData(404)]
        public void Get_Returns_NotFound_Test(int id)
        {
            Assert.Throws<AggregateException>(() => _client.Get(id));
        }

        [Fact]
        public async Task GetAsync_Test()
        {
            var res = await _client.GetAsync(TestData.Stock1.Id);
            Assert.NotNull(res);
            Assert.True(res is Stock);
            Assert.Equal(TestData.Stock1.Id, res.Id);
            Assert.Equal(TestData.Stock1.Name, res.Name);
        }

        [Theory]
        [InlineData(404)]
        public async Task GetAsync_Returns_NotFound_Test(int id)
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _client.GetAsync(id));
        }

        [Fact]
        public void Add_Test()
        {
            var item = new Stock() { Name = "StockToAdd" };

            var res = _client.Add(item);
            Assert.NotNull(res);
            Assert.True(res is Stock);
            Assert.NotEqual(0, res.Id);
            Assert.Equal(item.Name, res.Name);
        }

        [Fact]
        public void Add_Null_Item_Returns_ArgumentNullException_Test()
        {
            Assert.Throws<ArgumentNullException>(() => _client.Add(null!));
        }

        [Fact]
        public void Add_Exsisted_Item_Throws_InvalidOperationException_Test()
        {
            Stock item = new Stock() { Id = 1, Name = "Stock_1" };

            Assert.Throws<InvalidOperationException>(() => _client.Add(item));
        }

        [Fact]
        public async Task AddAsync_Test()
        {
            var item = new Stock() { Name = "StockToAdd" };

            var res = await _client.AddAsync(item);
            Assert.NotNull(res);
            Assert.True(res is Stock);
            Assert.NotEqual(0, res.Id);
            Assert.Equal(item.Name, res.Name);
        }

        [Fact]
        public async Task AddAsync_Null_Item_Returns_ArgumentNullException_Test()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _client.AddAsync(null!));
        }

        [Fact]
        public async Task AddAsync_Exsisted_Item_Test()
        {
            Stock item = new Stock() { Id = 1, Name = "Stock_1" };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _client.AddAsync(item));
        }

        [Theory]
        [InlineData("UpdatedItem")]
        public void Update_Test(string newName)
        {
            var item = new Stock() { Name = "ItemToAdd" };
            var res = _client.Add(item);
            item.Id = res.Id;

            var itemToUpdate = new Stock() { Name = newName };
            res = _client.Update(item.Id, itemToUpdate);

            Assert.NotNull(res);
            Assert.True(res is Stock);
            Assert.NotEqual(0, res.Id);
            Assert.Equal(itemToUpdate.Name, res.Name);
        }

        [Fact]
        public void Update_Null_Item_Returns_ArgumentNullException_Test()
        {
            Assert.Throws<ArgumentNullException>(() => _client.Update(404, null!));
        }

        [Theory]
        [InlineData(404, "ItemToUpdate")]
        public void Update_Returns_InvalidOperationException_Test(int id, string newName)
        {
            var itemToUpdate = new Stock() { Name = newName };
            Assert.Throws<InvalidOperationException>(() => _client.Update(id, itemToUpdate));
        }

        [Theory]
        [InlineData("UpdatedItem")]
        public async Task UpdateAsync_Test(string newName)
        {
            var item = new Stock() { Name = "ItemToAdd" };
            var res = await _client.AddAsync(item);
            item.Id = res.Id;

            var itemToUpdate = new Stock() { Name = newName };
            res = await _client.UpdateAsync(item.Id, itemToUpdate);

            Assert.NotNull(res);
            Assert.True(res is Stock);
            Assert.NotEqual(0, res.Id);
            Assert.Equal(itemToUpdate.Name, res.Name);
        }

        [Fact]
        public async Task UpdateAsync_Null_Item_Returns_ArgumentNullException_Test()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _client.UpdateAsync(404, null!));
        }

        [Theory]
        [InlineData(404, "ItemToUpdate")]
        public async Task UpdateAsync_Returns_InvalidOperationException_Test(int id, string newName)
        {
            var itemToUpdate = new Stock() { Name = newName };
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _client.UpdateAsync(id, itemToUpdate));
        }

        [Fact]
        public void Delete_Test()
        {
            var itemToAdd = new Stock() { Name = "ItemToAdd" };
            var addedItem = _client.Add(itemToAdd);

            var deletedItem = _client.Delete(addedItem.Id);

            Assert.NotNull(deletedItem);
            Assert.Equal(addedItem.Name, deletedItem.Name);
        }

        [Theory]
        [InlineData(404)]
        public void Detele_Returns_Exception_Test(int id)
        {
            Assert.Throws<ArgumentException>(() => _client.Delete(id));
        }

        [Fact]
        public async Task DeleteAsync_Test()
        {
            var itemToAdd = new Stock() { Name = "ItemToAdd" };
            var addedItem = await _client.AddAsync(itemToAdd);

            var deletedItem = await _client.DeleteAsync(addedItem.Id);

            Assert.NotNull(deletedItem);
            Assert.Equal(addedItem.Name, deletedItem.Name);
        }

        [Theory]
        [InlineData(404)]
        public async Task DeteleAsync_Returns_Exception_Test(int id)
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _client.DeleteAsync(id));
        }
    }
}
