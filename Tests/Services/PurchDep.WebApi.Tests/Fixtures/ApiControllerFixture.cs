using Microsoft.EntityFrameworkCore;
using Moq;
using PurchDep.Dal;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
namespace PurchDep.WebApi.Tests.Fixtures
{
    public class ApiControllerFixture
    {
        public Mock<ProductService> ProductServiceMock { get; }
        public Mock<SupplierService> SupplierServiceMock { get; }
        public Mock<StockService> StockServiceMock { get; }
        public ApiControllerFixture()
        {
            var productMappingService = new Mock<ProductMappingService>().Object;
            var suppliersProductMappingService = new Mock<SuppliersProductMappingService>().Object;
            var stocksProductMappingService = new Mock<StocksProductMappingService>().Object;
            var supplierMappingService = new Mock<SupplierMappingService>(suppliersProductMappingService).Object;
            var stockMappingService = new Mock<StockMappingService>(stocksProductMappingService).Object;

            var options = new DbContextOptions<PurchDepContext>();
            var purchDepContext = new Mock<PurchDepContext>(options).Object;

            var productRepository = new Mock<ProductRepository>(purchDepContext).Object;
            var supplierRepository = new Mock<SupplierRepository>(purchDepContext).Object;
            var stockRepository = new Mock<StockRepository>(purchDepContext).Object;

            ProductServiceMock = new(productRepository, productMappingService);
            SupplierServiceMock = new(supplierRepository, supplierMappingService);
            StockServiceMock = new(stockRepository, stockMappingService);
        }
    }
}
