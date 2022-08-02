using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Mapping;
using Services.PurchDep.Interfaces.Tests.Data;
using System.Threading.Tasks;
using Xunit;

using ProductDal = PurchDep.Dal.Entities.Product;

namespace Services.PurchDep.Interfaces.Tests.Mapping
{
    public class ProductMappingServiceTests
    {
        IMappingService<ProductDal, IProduct> _mapper;
        public ProductMappingServiceTests()
        {
            _mapper = new ProductMappingService();
        }
        [Fact]
        public void MapTest()
        {
            var res = _mapper.Map(TestData.Product1);
            Assert.True(res is IProduct);
            Assert.Equal(TestData.Product1.Id, res.Id);
            Assert.Equal(TestData.Product1.Name, res.Name);
            Assert.Equal(TestData.Product1.Price, res.Price);

            var mapBack = _mapper.Map(res);
            Assert.True(mapBack is ProductDal);
            Assert.Equal(TestData.Product1.Id, mapBack.Id);
            Assert.Equal(TestData.Product1.Name, mapBack.Name);
            Assert.Equal(TestData.Product1.Price, mapBack.Price);
        }

        [Fact]
        public async Task MapAsyncTest()
        {
            var res = await _mapper.MapAsync(TestData.Product2);
            Assert.True(res is IProduct);
            Assert.Equal(TestData.Product2.Id, res.Id);
            Assert.Equal(TestData.Product2.Name, res.Name);
            Assert.Equal(TestData.Product2.Price, res.Price);

            var mapBack = _mapper.Map(res);
            Assert.True(mapBack is ProductDal);
            Assert.Equal(TestData.Product2.Id, mapBack.Id);
            Assert.Equal(TestData.Product2.Name, mapBack.Name);
            Assert.Equal(TestData.Product2.Price, mapBack.Price);
        }
    }
}
