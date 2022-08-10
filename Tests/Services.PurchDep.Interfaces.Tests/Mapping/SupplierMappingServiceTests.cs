using PurchDep.Domain;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Mapping;
using Services.PurchDep.Interfaces.Tests.Data;
using System.Threading.Tasks;
using Xunit;

using SupplierDal = PurchDep.Dal.Entities.Supplier;
using SupplierDom = PurchDep.Domain.Supplier;

namespace Services.PurchDep.Interfaces.Tests.Mapping
{
    public class SupplierMappingServiceTests
    {
        IMappingService<SupplierDal, SupplierDom> _mapper;
        public SupplierMappingServiceTests()
        {
            _mapper = new SupplierMappingService();
        }

        [Fact]
        public void MapTest()
        {
            var res = _mapper.Map(TestData.Supplier1);
            Assert.True(res is SupplierDom);
            Assert.Equal(TestData.Supplier1.Id, res.Id);
            Assert.Equal(TestData.Supplier1.Name, res.Name);

            var mapBack = _mapper.Map(res);
            Assert.True(mapBack is SupplierDal);
            Assert.Equal(TestData.Supplier1.Id, mapBack.Id);
            Assert.Equal(TestData.Supplier1.Name, mapBack.Name);
        }

        [Fact]
        public async Task MapAsyncTest()
        {
            var res = await _mapper.MapAsync(TestData.Supplier2);
            Assert.True(res is SupplierDom);
            Assert.Equal(TestData.Supplier2.Id, res.Id);
            Assert.Equal(TestData.Supplier2.Name, res.Name);

            var mapBack = _mapper.Map(res);
            Assert.True(mapBack is SupplierDal);
            Assert.Equal(TestData.Supplier2.Id, mapBack.Id);
            Assert.Equal(TestData.Supplier2.Name, mapBack.Name);
        }

        [Fact]
        public void MapRangeTest()
        {
            var source = TestData.AllSuppliers;
            var res = _mapper.MapRange(source);

            Assert.Equal(source.Count, res.Count);
            foreach (var item in res)
            {
                Assert.True(item is SupplierDom);
            }

            var mapBack = _mapper.MapRange(res);
            Assert.Equal(source.Count, mapBack.Count);
            foreach (var item in mapBack)
            {
                Assert.True(item is SupplierDal);
            }
        }

        [Fact]
        public async Task MapRangeAsyncTest()
        {
            var source = TestData.AllSuppliers;
            var res = await _mapper.MapRangeAsync(source);

            Assert.Equal(source.Count, res.Count);
            foreach (var item in res)
            {
                Assert.True(item is SupplierDom);
            }

            var mapBack = await _mapper.MapRangeAsync(res);
            Assert.Equal(source.Count, mapBack.Count);
            foreach (var item in mapBack)
            {
                Assert.True(item is SupplierDal);
            }
        }
    }
}
