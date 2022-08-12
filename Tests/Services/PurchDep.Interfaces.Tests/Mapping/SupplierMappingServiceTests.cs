using PurchDep.Interfaces.Mapping;
using Xunit;

namespace PurchDep.Interfaces.Tests.Mapping
{
    public class SupplierMappingServiceTests
    {
        SupplierMappingService _mapper;
        public SupplierMappingServiceTests()
        {
            var innerMapper = new SuppliersProductMappingService();
            _mapper = new(innerMapper);
        }

        [Fact]
        public void Map_ToDom_Test()
        {
            var result = _mapper.Map(TestData.TestData.SupplierDal_1);
            var sourceProducts = TestData.TestData.SupplierDal_1.SuppliersProducts;
            var resultProducts = result.SuppliersProducts;

            Assert.Equal(TestData.TestData.SupplierDal_1.Id, result.Id);
            Assert.Equal(TestData.TestData.SupplierDal_1.Name, result.Name);
            Assert.Equal(sourceProducts.Count, resultProducts.Count);
        }

        [Fact]
        public void Map_ToDal_Test()
        {
            var result = _mapper.Map(TestData.TestData.SupplierDom_1);
            var sourceProducts = TestData.TestData.SupplierDom_1.SuppliersProducts;
            var resultProducts = result.SuppliersProducts;

            Assert.Equal(TestData.TestData.SupplierDom_1.Id, result.Id);
            Assert.Equal(TestData.TestData.SupplierDom_1.Name, result.Name);
            Assert.Equal(sourceProducts.Count, resultProducts.Count);
        }
    }
}
