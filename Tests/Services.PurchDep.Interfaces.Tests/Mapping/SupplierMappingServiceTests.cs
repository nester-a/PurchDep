using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Mapping;
using Services.PurchDep.Interfaces.Tests.Data;
using System.Threading.Tasks;
using Xunit;

using SupplierDal = PurchDep.Dal.Entities.Supplier;

namespace Services.PurchDep.Interfaces.Tests.Mapping
{
    public class SupplierMappingServiceTests
    {
        IMappingService<SupplierDal, ISupplier> _mapper;
        public SupplierMappingServiceTests()
        {
            _mapper = new SupplierMappingService();
        }

        [Fact]
        public void MapTest()
        {
            var res = _mapper.Map(TestData.Supplier1);
            Assert.True(res is ISupplier);
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
            Assert.True(res is ISupplier);
            Assert.Equal(TestData.Supplier2.Id, res.Id);
            Assert.Equal(TestData.Supplier2.Name, res.Name);

            var mapBack = _mapper.Map(res);
            Assert.True(mapBack is SupplierDal);
            Assert.Equal(TestData.Supplier2.Id, mapBack.Id);
            Assert.Equal(TestData.Supplier2.Name, mapBack.Name);
        }
    }
}
