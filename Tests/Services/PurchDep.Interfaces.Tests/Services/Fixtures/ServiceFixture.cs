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

namespace PurchDep.Interfaces.Tests.Services.Fixtures
{
    public class ServiceFixture
    {
        List<ProductDal> Products;
        List<SupplierDal> Suppliers;
        List<StockDal> Stocks;
        public Mock<ProductRepository> ProductRepositoryMock { get; }
        public Mock<SupplierRepository> SupplierRepositoryMock { get; }
        public Mock<StockRepository> StockRepositoryMock { get; }

        public Mock<ProductMappingService> ProductMappingServiceMock { get; }
        public Mock<SupplierMappingService> SupplierMappingServiceMock { get; }
        public Mock<StockMappingService> StockMappingServiceMock { get; }

        public ServiceFixture()
        {
            Products = new();
            Suppliers = new();
            Stocks = new();

            Products.Add(TestData.TestData.ProductDal_1);
            Suppliers.Add(TestData.TestData.SupplierDal_1);
            Stocks.Add(TestData.TestData.StockDal_1);

            ProductRepositoryMock = new Mock<ProductRepository>();
            ProductRepositoryMock.Setup(o => o.GetAll()).Returns(Products);
            ProductRepositoryMock.Setup(o => o.Get(It.IsAny<int>())).Returns(new ProductDal() { Name = "Getted_FromMockRepository_ProductObj" });
            ProductRepositoryMock.Setup(o => o.Delete(It.IsAny<int>())).Returns(new ProductDal() { Name = "Deleted_FromMockRepository_ProductObj" });
            ProductRepositoryMock.Setup(o => o.Update(It.IsAny<int>(), It.IsAny<ProductDal>())).Returns(new ProductDal() { Name = "Updated_FromMockRepository_ProductObj" });
            ProductRepositoryMock.Setup(o => o.Add(It.IsAny<ProductDal>())).Returns(new ProductDal() { Name = "Added_FromMockRepository_ProductObj" });

            SupplierRepositoryMock = new Mock<SupplierRepository>();
            SupplierRepositoryMock.Setup(o => o.GetAll()).Returns(Suppliers);
            SupplierRepositoryMock.Setup(o => o.Get(It.IsAny<int>())).Returns(new SupplierDal() { Name = "Getted_FromMockRepository_SupplierObj" });
            SupplierRepositoryMock.Setup(o => o.Delete(It.IsAny<int>())).Returns(new SupplierDal() { Name = "Deleted_FromMockRepository_SupplierObj" });
            SupplierRepositoryMock.Setup(o => o.Update(It.IsAny<int>(), It.IsAny<SupplierDal>())).Returns(new SupplierDal() { Name = "Updated_FromMockRepository_SupplierObj" });
            SupplierRepositoryMock.Setup(o => o.Add(It.IsAny<SupplierDal>())).Returns(new SupplierDal() { Name = "Added_FromMockRepository_SupplierObj" });

            StockRepositoryMock = new Mock<StockRepository>();
            StockRepositoryMock.Setup(o => o.GetAll()).Returns(Stocks);
            StockRepositoryMock.Setup(o => o.Get(It.IsAny<int>())).Returns(new StockDal() { Name = "Getted_FromMockRepository_StockObj" });
            StockRepositoryMock.Setup(o => o.Delete(It.IsAny<int>())).Returns(new StockDal() { Name = "Deleted_FromMockRepository_StockObj" });
            StockRepositoryMock.Setup(o => o.Update(It.IsAny<int>(), It.IsAny<StockDal>())).Returns(new StockDal() { Name = "Updated_FromMockRepository_StockObj" });
            StockRepositoryMock.Setup(o => o.Add(It.IsAny<StockDal>())).Returns(new StockDal() { Name = "Added_FromMockRepository_StockObj" });

            ProductMappingServiceMock = new Mock<ProductMappingService>();
            ProductMappingServiceMock.Setup(o => o.Map(It.IsAny<ProductDal>())).Returns(new ProductDom() { Name = "Mapped_FromMockMapper_ProductDomObj" });
            ProductMappingServiceMock.Setup(o => o.Map(It.IsAny<ProductDom>())).Returns(new ProductDal() { Name = "Mapped_FromMockMapper_ProductDalObj" });

            SupplierMappingServiceMock = new Mock<SupplierMappingService>();
            SupplierMappingServiceMock.Setup(o => o.Map(It.IsAny<SupplierDal>())).Returns(new SupplierDom() { Name = "Mapped_FromMockMapper_SupplierDomObj" });
            SupplierMappingServiceMock.Setup(o => o.Map(It.IsAny<SupplierDom>())).Returns(new SupplierDal() { Name = "Mapped_FromMockMapper_SupplierDalObj" });

            StockMappingServiceMock = new Mock<StockMappingService>();
            StockMappingServiceMock.Setup(o => o.Map(It.IsAny<StockDal>())).Returns(new StockDom() { Name = "Mapped_FromMockMapper_StockDomObj" });
            StockMappingServiceMock.Setup(o => o.Map(It.IsAny<StockDom>())).Returns(new StockDal() { Name = "Mapped_FromMockMapper_StockDalObj" });
        }
    }
}
