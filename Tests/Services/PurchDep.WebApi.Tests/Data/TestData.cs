using System.Collections.Generic;

using ProductDal = PurchDep.Dal.Entities.Product;
using SupplierDal = PurchDep.Dal.Entities.Supplier;
using ProductDom = PurchDep.Domain.Product;
using SupplierDom = PurchDep.Domain.Supplier;

namespace PurchDep.WebApi.Tests.Data
{
    public static class TestData
    {
        public static ICollection<ProductDal> AllProducts = new List<ProductDal>();
        public static ICollection<SupplierDal> AllSuppliers = new List<SupplierDal>();
        public static ProductDal Product1 { get; } = new ProductDal() { Name = "Product_1", };
        public static ProductDal Product2 { get; } = new ProductDal() { Name = "Product_2", };
        public static ProductDom Product3 { get; } = new ProductDom() { Name = "Product_3", };
        public static ProductDom Product33 { get; } = new ProductDom() { Name = "Product_33", };
        public static ProductDom Product4 { get; } = new ProductDom() { Name = "Product_4", };
        public static ProductDom Product44 { get; } = new ProductDom() { Name = "Product_44", };
        public static ProductDom Product5 { get; } = new ProductDom() { Name = "Product_5", };
        public static ProductDom Product55 { get; } = new ProductDom() { Name = "Product_55", };
        public static ProductDom Product6 { get; } = new ProductDom() { Name = "Product_6", };
        public static ProductDom Product66 { get; } = new ProductDom() { Name = "Product_66", };
        public static SupplierDal Supplier1 { get; } = new SupplierDal() { Name = "Supplier_1" };
        public static SupplierDal Supplier2 { get; } = new SupplierDal() { Name = "Supplier_2" };
        public static SupplierDom Supplier3 { get; } = new SupplierDom() { Name = "Supplier_3" };
        public static SupplierDom Supplier4 { get; } = new SupplierDom() { Name = "Supplier_4" };
        public static SupplierDom Supplier5 { get; } = new SupplierDom() { Name = "Supplier_5" };
        public static SupplierDom Supplier6 { get; } = new SupplierDom() { Name = "Supplier_6" };
        static TestData()
        {
            AllProducts.Add(Product1);
            AllProducts.Add(Product2);


            AllSuppliers.Add(Supplier1);
            AllSuppliers.Add(Supplier2);
        }
    }
}
