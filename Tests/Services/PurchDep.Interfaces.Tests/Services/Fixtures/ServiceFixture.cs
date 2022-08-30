using Moq;
using PurchDep.Interfaces.Repositories;
using System.Collections.Generic;
using ProductDal = PurchDep.Dal.Entities.Product;
using ProductDom = PurchDep.Domain.Product;
using SupplierDal = PurchDep.Dal.Entities.Supplier;
using SupplierDom = PurchDep.Domain.Supplier;
using StockDal = PurchDep.Dal.Entities.Stock;
using StockDom = PurchDep.Domain.Stock;
using PurchDep.Interfaces.Mapping;
using System;
using PurchDep.Dal;
using Microsoft.EntityFrameworkCore;

namespace PurchDep.Interfaces.Tests.Services.Fixtures
{
    public class ServiceFixture : IDisposable
    {
        public List<ProductDal> ProductsDal { get; }
        public List<ProductDom> ProductsDom { get; }
        public List<SupplierDal> SuppliersDal { get; }
        public List<SupplierDom> SuppliersDom { get; }
        public List<StockDal> StocksDal { get; }
        public List<StockDom> StocksDom { get; }


        public Mock<ProductRepository> ProductRepositoryMock { get; }
        public Mock<SupplierRepository> SupplierRepositoryMock { get; }
        public Mock<StockRepository> StockRepositoryMock { get; }

        public Mock<ProductMappingService> ProductMappingServiceMock { get; }
        public Mock<SupplierMappingService> SupplierMappingServiceMock { get; }
        public Mock<StockMappingService> StockMappingServiceMock { get; }

        public ServiceFixture()
        {
            ProductsDal = new();
            ProductsDom = new();
            SuppliersDal = new();
            SuppliersDom = new();
            StocksDal = new();
            StocksDom = new();

            ProductsDal.Add(TestData.TestData.ProductDal_1);
            ProductsDom.Add(TestData.TestData.ProductDom_1);
            SuppliersDal.Add(TestData.TestData.SupplierDal_1);
            SuppliersDom.Add(TestData.TestData.SupplierDom_1);
            StocksDal.Add(TestData.TestData.StockDal_1);
            StocksDom.Add(TestData.TestData.StockDom_1);

            var options = new DbContextOptions<PurchDepContext>();
            var purchDepContextMockObject = new Mock<PurchDepContext>(options).Object;

            ProductRepositoryMock = new Mock<ProductRepository>(purchDepContextMockObject);
            SupplierRepositoryMock = new Mock<SupplierRepository>(purchDepContextMockObject);
            StockRepositoryMock = new Mock<StockRepository>(purchDepContextMockObject);

            ProductMappingServiceMock = new Mock<ProductMappingService>();

            var SuppliersProductMappingServiceMockObject = new Mock<SuppliersProductMappingService>().Object;
            SupplierMappingServiceMock = new Mock<SupplierMappingService>(SuppliersProductMappingServiceMockObject);

            var StocksProductMappingServiceMockObject = new Mock<StocksProductMappingService>().Object;
            StockMappingServiceMock = new Mock<StockMappingService>(StocksProductMappingServiceMockObject);
        }

        public void Dispose()
        {
            ProductsDal?.Clear();
            ProductsDom?.Clear();
            SuppliersDal?.Clear();
            SuppliersDom?.Clear();
            StocksDal?.Clear();
            StocksDom?.Clear();
        }
    }
}
