using PurchDep.Dal.Entities;
using System.Collections.Generic;

namespace Services.PurchDep.Interfaces.Tests.Data
{
    public static class ServicesTestData
    {
        public static ICollection<Product> AllProducts = new List<Product>();
        public static ICollection<Supplier> AllSuppliers = new List<Supplier>();
        public static Product Product1 { get; } = new Product() { Id = 1, Name = "Product_1", Price = 1.11m, };
        public static Product Product2 { get; } = new Product() { Id = 2, Name = "Product_2", Price = 2.22m, };
        public static Supplier Supplier1 { get; } = new Supplier() { Id = 1, Name = "Supplier_1" };
        public static Supplier Supplier2 { get; } = new Supplier() { Id = 2, Name = "Supplier_2" };
        static ServicesTestData()
        {
            AllProducts.Add(Product1);
            AllProducts.Add(Product2);

            Supplier1.Products.Add(Product1);
            Supplier2.Products.Add(Product1);
            Supplier2.Products.Add(Product2);

            AllSuppliers.Add(Supplier1);
            AllSuppliers.Add(Supplier2);
        }
    }
}
