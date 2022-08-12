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

namespace PurchDep.Interfaces.Tests.TestData
{
    public static class TestData
    {
        public static ProductDal ProductDal_1 { get; } = new()
        {
            Name = "ProductDal_1",
        };
        public static ProductDom ProductDom_1 { get; } = new()
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

        public static SupplierDom SupplierDom_1 { get; } = new()
        {
            Name = "SupplierDom_1",
        };
        public static SuppliersProductDom SuppliersProductDom_1 { get; } = new()
        {
            Id = ProductDom_1.Id,
            Name = ProductDom_1.Name,
            SupplierId = SupplierDom_1.Id,
        };

        static TestData()
        {
            SupplierDal_1.SuppliersProducts.Add(SuppliersProductDal_1);
            SupplierDom_1.SuppliersProducts.Add(SuppliersProductDom_1);
        }
    }
}
