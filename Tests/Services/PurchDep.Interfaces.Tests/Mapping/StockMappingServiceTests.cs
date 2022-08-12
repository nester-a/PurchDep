using PurchDep.Interfaces.Mapping;
using Xunit;

namespace PurchDep.Interfaces.Tests.Mapping
{
    public class StockMappingServiceTests
    {
        StockMappingService _mapper;
        public StockMappingServiceTests()
        {
            var innerMapper = new StocksProductMappingService();
            _mapper = new(innerMapper);
        }

        [Fact]
        public void Map_ToDom_Test()
        {
            var result = _mapper.Map(TestData.TestData.StockDal_1);
            var sourceProducts = TestData.TestData.StockDal_1.StocksProducts;
            var resultProducts = result.StocksProducts;

            Assert.Equal(TestData.TestData.StockDal_1.Id, result.Id);
            Assert.Equal(TestData.TestData.StockDal_1.Name, result.Name);
            Assert.Equal(sourceProducts.Count, resultProducts.Count);
        }

        [Fact]
        public void Map_ToDal_Test()
        {
            var result = _mapper.Map(TestData.TestData.StockDom_1);
            var sourceProducts = TestData.TestData.StockDom_1.StocksProducts;
            var resultProducts = result.StocksProducts;

            Assert.Equal(TestData.TestData.StockDom_1.Id, result.Id);
            Assert.Equal(TestData.TestData.StockDom_1.Name, result.Name);
            Assert.Equal(sourceProducts.Count, resultProducts.Count);
        }
    }
}
