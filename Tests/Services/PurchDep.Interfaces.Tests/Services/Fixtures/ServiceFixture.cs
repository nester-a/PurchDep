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
        List<ProductDal> Products;
        List<SupplierDal> Suppliers;
        List<StockDal> Stocks;

        PurchDepContext PurchDepContextMockObject { get; }

        public Mock<ProductRepository> ProductRepositoryMock { get; }
        public ProductRepository ProductRepositoryMockObject { get; }
        public Mock<SupplierRepository> SupplierRepositoryMock { get; }
        public SupplierRepository SupplierRepositoryMockObject { get; }
        public Mock<StockRepository> StockRepositoryMock { get; }
        public StockRepository StockRepositoryMockObject { get; }

        public Mock<ProductMappingService> ProductMappingServiceMock { get; }
        public ProductMappingService ProductMappingServiceMockObject { get; }
        public Mock<SupplierMappingService> SupplierMappingServiceMock { get; }
        public SupplierMappingService SupplierMappingServiceMockObject { get; }
        public Mock<StockMappingService> StockMappingServiceMock { get; }
        public StockMappingService StockMappingServiceMockObject { get; }

        public ServiceFixture()
        {
            Products = new();
            Suppliers = new();
            Stocks = new();

            Products.Add(TestData.TestData.ProductDal_1);
            Suppliers.Add(TestData.TestData.SupplierDal_1);
            Stocks.Add(TestData.TestData.StockDal_1);

            DbContextOptions<PurchDepContext> options = new DbContextOptions<PurchDepContext>();
            PurchDepContextMockObject = new Mock<PurchDepContext>(options).Object;

            ProductRepositoryMock = new Mock<ProductRepository>(PurchDepContextMockObject);
            ProductRepositoryMock.Setup(o => o.GetAll()).Returns(Products);
            ProductRepositoryMock.Setup(o => o.Get(It.IsAny<int>())).Returns(new ProductDal() { Id = 1, Name = "Getted_FromMockRepository_ProductObj" });
            ProductRepositoryMock.Setup(o => o.Update(It.IsAny<int>(), It.IsAny<ProductDal>())).Returns(new ProductDal() { Id = 1, Name = "Updated_FromMockRepository_ProductObj" });
            ProductRepositoryMockObject = ProductRepositoryMock.Object;

            SupplierRepositoryMock = new Mock<SupplierRepository>(PurchDepContextMockObject);
            SupplierRepositoryMock.Setup(o => o.GetAll()).Returns(Suppliers);
            SupplierRepositoryMock.Setup(o => o.Get(It.IsAny<int>())).Returns(new SupplierDal() { Id = 1, Name = "Getted_FromMockRepository_SupplierObj" });
            SupplierRepositoryMock.Setup(o => o.Delete(It.IsAny<int>())).Returns(new SupplierDal() { Id = 1, Name = "Deleted_FromMockRepository_SupplierObj" });
            SupplierRepositoryMock.Setup(o => o.Update(It.IsAny<int>(), It.IsAny<SupplierDal>())).Returns(new SupplierDal() { Id = 1, Name = "Updated_FromMockRepository_SupplierObj" });
            SupplierRepositoryMock.Setup(o => o.Add(It.IsAny<SupplierDal>())).Returns(new SupplierDal() { Id = 1, Name = "Added_FromMockRepository_SupplierObj" });
            SupplierRepositoryMockObject = SupplierRepositoryMock.Object;

            StockRepositoryMock = new Mock<StockRepository>(PurchDepContextMockObject);
            StockRepositoryMock.Setup(o => o.GetAll()).Returns(Stocks);
            StockRepositoryMock.Setup(o => o.Get(It.IsAny<int>())).Returns(new StockDal() { Id = 1, Name = "Getted_FromMockRepository_StockObj" });
            StockRepositoryMock.Setup(o => o.Delete(It.IsAny<int>())).Returns(new StockDal() { Id = 1, Name = "Deleted_FromMockRepository_StockObj" });
            StockRepositoryMock.Setup(o => o.Update(It.IsAny<int>(), It.IsAny<StockDal>())).Returns(new StockDal() { Id = 1, Name = "Updated_FromMockRepository_StockObj" });
            StockRepositoryMock.Setup(o => o.Add(It.IsAny<StockDal>())).Returns(new StockDal() { Id = 1, Name = "Added_FromMockRepository_StockObj" });
            StockRepositoryMockObject = StockRepositoryMock.Object;

            ProductMappingServiceMock = new Mock<ProductMappingService>();

            var SuppliersProductMappingServiceMockObject = new Mock<SuppliersProductMappingService>().Object;

            SupplierMappingServiceMock = new Mock<SupplierMappingService>(SuppliersProductMappingServiceMockObject);
            SupplierMappingServiceMock.Setup(o => o.Map(It.IsAny<SupplierDal>())).Returns(new SupplierDom() { Id = 1, Name = "Mapped_FromMockMapper_SupplierDomObj" });
            SupplierMappingServiceMock.Setup(o => o.Map(It.IsAny<SupplierDom>())).Returns(new SupplierDal() { Id = 1, Name = "Mapped_FromMockMapper_SupplierDalObj" });
            SupplierMappingServiceMockObject = SupplierMappingServiceMock.Object;

            var StocksProductMappingServiceMockObject = new Mock<StocksProductMappingService>().Object;

            StockMappingServiceMock = new Mock<StockMappingService>(StocksProductMappingServiceMockObject);
            StockMappingServiceMock.Setup(o => o.Map(It.IsAny<StockDal>())).Returns(new StockDom() { Id = 1, Name = "Mapped_FromMockMapper_StockDomObj" });
            StockMappingServiceMock.Setup(o => o.Map(It.IsAny<StockDom>())).Returns(new StockDal() { Id = 1, Name = "Mapped_FromMockMapper_StockDalObj" });
            StockMappingServiceMockObject = StockMappingServiceMock.Object;
        }

        public void Dispose()
        {
            Products?.Clear();
            Suppliers?.Clear();
            Stocks?.Clear();
        }
    }
}
