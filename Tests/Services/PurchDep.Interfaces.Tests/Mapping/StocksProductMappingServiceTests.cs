using PurchDep.Interfaces.Mapping;
using Xunit;
namespace PurchDep.Interfaces.Tests.Mapping
{
    public class StocksProductMappingServiceTests
    {
        StocksProductMappingService _mapper;
        public StocksProductMappingServiceTests()
        {
            _mapper = new();
        }

        [Fact]
        public void Map_ToDom_Test()
        {
            var result = _mapper.Map(TestData.TestData.StocksProductDal_1);

            Assert.Equal(TestData.TestData.StocksProductDal_1.ProductId, result.Id);
            Assert.Equal(TestData.TestData.StocksProductDal_1.Product.Name, result.Name);
            Assert.Equal(TestData.TestData.StocksProductDal_1.SupplierId, result.SupplierId);
            Assert.Equal(TestData.TestData.StocksProductDal_1.StockId, result.StockId);
            Assert.Equal(TestData.TestData.StocksProductDal_1.Quantity, result.Quantity);
            Assert.NotEqual(0, result.SuppliersPrice);
        }

        [Fact]
        public void Map_ToDal_Test()
        {
            var result = _mapper.Map(TestData.TestData.StocksProductDom_1);

            Assert.Equal(TestData.TestData.StocksProductDom_1.Id, result.ProductId);
            Assert.Equal(TestData.TestData.StocksProductDom_1.StockId, result.StockId);
            Assert.Equal(TestData.TestData.StocksProductDom_1.SupplierId, result.SupplierId);
            Assert.Equal(TestData.TestData.StocksProductDom_1.Quantity, result.Quantity);
        }
    }
}
