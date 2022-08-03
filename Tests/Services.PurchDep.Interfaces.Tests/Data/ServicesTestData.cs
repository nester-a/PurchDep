using PurchDep.Dal.Entities;
using System.Collections.Generic;

using ProductDal = PurchDep.Dal.Entities.Product;
using SupplierDal = PurchDep.Dal.Entities.Supplier;
using ProductDom = PurchDep.Domain.Product;
using SupplierDom = PurchDep.Domain.Supplier;

namespace Services.PurchDep.Interfaces.Tests.Data
{
    public static class ServicesTestData
    {
        public static ICollection<Product> AllProducts = new List<Product>();
        public static ICollection<Supplier> AllSuppliers = new List<Supplier>();
        public static ProductDal Product1 { get; } = new ProductDal() { Name = "Product_1", Price = 1.11m, };
        public static ProductDal Product2 { get; } = new ProductDal() { Name = "Product_2", Price = 2.22m, };
        public static ProductDom Product3 { get; } = new ProductDom() { Name = "Product_3", Price = 3.33m, };
        public static ProductDom Product33 { get; } = new ProductDom() { Name = "Product_33", Price = 3.33m, };
        public static ProductDom Product4 { get; } = new ProductDom() { Name = "Product_4", Price = 4.44m, };
        public static ProductDom Product44 { get; } = new ProductDom() { Name = "Product_44", Price = 4.44m, };
        public static ProductDom Product5 { get; } = new ProductDom() { Name = "Product_5", Price = 5.55m, };
        public static ProductDom Product55 { get; } = new ProductDom() { Name = "Product_55", Price = 5.55m, };
        public static ProductDom Product6 { get; } = new ProductDom() { Name = "Product_6", Price = 6.66m, };
        public static ProductDom Product66 { get; } = new ProductDom() { Name = "Product_66", Price = 6.66m, };
        public static SupplierDal Supplier1 { get; } = new SupplierDal() { Name = "Supplier_1" };
        public static SupplierDal Supplier2 { get; } = new SupplierDal() { Name = "Supplier_2" };
        public static SupplierDom Supplier3 { get; } = new SupplierDom() { Name = "Supplier_3" };
        public static SupplierDom Supplier4 { get; } = new SupplierDom() { Name = "Supplier_4" };
        public static SupplierDom Supplier5 { get; } = new SupplierDom() { Name = "Supplier_5" };
        public static SupplierDom Supplier6 { get; } = new SupplierDom() { Name = "Supplier_6" };
        static ServicesTestData()
        {
            AllProducts.Add(Product1);
            AllProducts.Add(Product2);

            Supplier1.Products.Add(Product1);
            Supplier2.Products.Add(Product2);
            Supplier3.Products.Add(Product33);
            Supplier4.Products.Add(Product44);
            Supplier5.Products.Add(Product55);
            Supplier6.Products.Add(Product66);

            AllSuppliers.Add(Supplier1);
            AllSuppliers.Add(Supplier2);
        }
    }
}
