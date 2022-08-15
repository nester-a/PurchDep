using ProductDal = PurchDep.Dal.Entities.Product;
using SuppliersProductDal = PurchDep.Dal.Entities.SuppliersProduct;
using StocksProductDal = PurchDep.Dal.Entities.StocksProduct;
using SupplierDal = PurchDep.Dal.Entities.Supplier;
using StockDal = PurchDep.Dal.Entities.Stock;
using ProductDom = PurchDep.Domain.Product;
using SuppliersProductDom = PurchDep.Domain.SuppliersProduct;
using StocksProductDom = PurchDep.Domain.StocksProduct;
using SupplierDom = PurchDep.Domain.Supplier;
using StockDom = PurchDep.Domain.Stock;
using System.Collections.Generic;

namespace PurchDep.WebApi.Tests.Data
{
    public static class TestData
    {
        public static ICollection<ProductDom> ProductsDom { get; } = new List<ProductDom>();
        public static ICollection<SupplierDom> SuppliersDom { get; } = new List<SupplierDom>();
        public static ICollection<StockDom> StocksDom { get; } = new List<StockDom>();
        public static ProductDal ProductDal_1 { get; } = new()
        {
            Name = "ProductDal_1",
        };
        public static SupplierDal SupplierDal_1 { get; } = new()
        {
            Name = "SupplierDal_1",
        };
        public static SuppliersProductDal SuppliersProductDal_1 { get; } = new()
        {
            Price = 10m,
            Product = ProductDal_1,
            ProductId = ProductDal_1.Id,
            Supplier = SupplierDal_1,
            SupplierId = SupplierDal_1.Id,
        };
        public static StockDal StockDal_1 { get; } = new()
        {
            Name = "StockDal_1",
        };
        public static StocksProductDal StocksProductDal_1 { get; } = new()
        {
            Product = ProductDal_1,
            ProductId = ProductDal_1.Id,
            Supplier = SupplierDal_1,
            SupplierId = SupplierDal_1.Id,
            Stock = StockDal_1,
            StockId = StockDal_1.Id,
            Quantity = 10,
        };
        public static ProductDom ProductDom_1 { get; } = new()
        {
            Name = "ProductDal_1",
        };
        public static ProductDom ProductDom_ForAdding { get; } = new()
        {
            Name = "ProductDal_ForAdding",
        };
        public static ProductDom ProductDom_ForUpdating { get; } = new()
        {
            Name = "ProductDom_ForUpdating",
        };
        public static ProductDom ProductDom_ForDeleting { get; } = new()
        {
            Name = "ProductDom_ForDeleting",
        };
        public static SupplierDom SupplierDom_1 { get; } = new()
        {
            Name = "SupplierDom_1",
        };
        public static SupplierDom SupplierDom_ForAdding { get; } = new()
        {
            Name = "SupplierDom_ForAdding",
        };
        public static SupplierDom SupplierDom_ForUpdating { get; } = new()
        {
            Name = "SupplierDom_ForUpdating",
        };
        public static SupplierDom SupplierDom_ForDeleting { get; } = new()
        {
            Name = "SupplierDom_ForDeleting",
        };
        public static SuppliersProductDom SuppliersProductDom_1 { get; } = new()
        {
            Id = ProductDom_1.Id,
            Name = ProductDom_1.Name,
            SupplierId = SupplierDom_1.Id,
        };
        public static StockDom StockDom_1 { get; } = new()
        {
            Name = "StockDom_1",
        };
        public static StockDom StockDom_ForAdding { get; } = new()
        {
            Name = "StockDom_ForAdding",
        };
        public static StockDom StockDom_ForUpdating { get; } = new()
        {
            Name = "StockDom_ForUpdating",
        };
        public static StockDom StockDom_ForDeleting { get; } = new()
        {
            Name = "StockDom_ForDeleting",
        };
        public static StocksProductDom StocksProductDom_1 { get; } = new()
        {
            Id = ProductDom_1.Id,
            Name = ProductDom_1.Name,
            SupplierId = SupplierDom_1.Id,
            SuppliersPrice = SuppliersProductDom_1.SuppliersPrice,
            StockId = StockDom_1.Id,
            Quantity = 10,
        };

        static TestData()
        {
            SupplierDal_1.SuppliersProducts.Add(SuppliersProductDal_1);
            SupplierDom_1.SuppliersProducts.Add(SuppliersProductDom_1);

            StockDal_1.StocksProducts.Add(StocksProductDal_1);
            StockDom_1.StocksProducts.Add(StocksProductDom_1);

            ProductsDom.Add(ProductDom_1);
            SuppliersDom.Add(SupplierDom_1);
            StocksDom.Add(StockDom_1);
        }
    }
}
