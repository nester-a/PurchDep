using PurchDep.Interfaces.Mapping;
using Xunit;

namespace PurchDep.Interfaces.Tests.Mapping
{
    public class ProductMappingServiceTests
    {
        ProductMappingService _mapper; 
        public ProductMappingServiceTests()
        {
            _mapper = new ProductMappingService();
        }

        [Fact]
        public void Map_ToDom_Test()
        {
            var result = _mapper.Map(TestData.TestData.ProductDal_1);

            Assert.Equal(TestData.TestData.ProductDal_1.Id, result.Id);
            Assert.Equal(TestData.TestData.ProductDal_1.Name, result.Name);
        }

        [Fact]
        public void Map_ToDal_Test()
        {
            var result = _mapper.Map(TestData.TestData.ProductDom_1);

            Assert.Equal(TestData.TestData.ProductDom_1.Id, result.Id);
            Assert.Equal(TestData.TestData.ProductDom_1.Name, result.Name);
        }
    }
}
