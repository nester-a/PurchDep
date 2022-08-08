using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Mapping;
using Services.PurchDep.Interfaces.Tests.Data;
using System.Threading.Tasks;
using Xunit;

using ProductDal = PurchDep.Dal.Entities.Product;
using ProductDom = PurchDep.Domain.Product;

namespace Services.PurchDep.Interfaces.Tests.Mapping
{
    public class ProductMappingServiceTests
    {
        IMappingService<ProductDal, ProductDom> _mapper;
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

        [Fact]
        public void MapRangeTest()
        {
            var source = TestData.AllProducts;
            var res = _mapper.MapRange(source);

            Assert.Equal(source.Count, res.Count);
            foreach (var item in res)
            {
                Assert.True(item is IProduct);
            }

            var mapBack = _mapper.MapRange(res);
            Assert.Equal(source.Count, mapBack.Count);
            foreach (var item in mapBack)
            {
                Assert.True(item is ProductDal);
            }
        }

        [Fact]
        public async Task MapRangeAsyncTest()
        {
            var source = TestData.AllProducts;
            var res = await _mapper.MapRangeAsync(source);

            Assert.Equal(source.Count, res.Count);
            foreach (var item in res)
            {
                Assert.True(item is IProduct);
            }

            var mapBack = await _mapper.MapRangeAsync(res);
            Assert.Equal(source.Count, mapBack.Count);
            foreach (var item in mapBack)
            {
                Assert.True(item is ProductDal);
            }
        }
    }
}
