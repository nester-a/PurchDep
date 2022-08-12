using PurchDep.Interfaces.Mapping;
using Xunit;

namespace PurchDep.Interfaces.Tests.Mapping
{
    public class SuppliersProductMappingServiceTests
    {
        SuppliersProductMappingService _mapper;
        public SuppliersProductMappingServiceTests()
        {
            _mapper = new SuppliersProductMappingService();
        }

        [Fact]
        public void Map_ToDom_Test()
        {
            var result = _mapper.Map(TestData.TestData.SuppliersProductDal_1);

            Assert.Equal(TestData.TestData.SuppliersProductDal_1.ProductId, result.Id);
            Assert.Equal(TestData.TestData.SuppliersProductDal_1.Price, result.SuppliersPrice);
            Assert.Equal(TestData.TestData.SuppliersProductDal_1.SupplierId, result.SupplierId);
            Assert.Equal(TestData.TestData.SuppliersProductDal_1.Product.Name, result.Name);
        }

        [Fact]
        public void Map_ToDal_Test()
        {
            var result = _mapper.Map(TestData.TestData.SuppliersProductDom_1);

            Assert.Equal(TestData.TestData.SuppliersProductDom_1.Id, result.ProductId);
            Assert.Equal(TestData.TestData.SuppliersProductDom_1.SupplierId, result.SupplierId);
            Assert.Equal(TestData.TestData.SuppliersProductDom_1.SuppliersPrice, result.Price);
        }
    }
}
