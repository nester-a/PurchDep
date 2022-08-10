using PurchDep.Dal.Entities;
using System.Collections.Generic;

namespace Data.PurchDep.Dal.Tests.Data
{
    public static class TestData
    {
        public static ICollection<Product> AllProducts = new List<Product>();
        public static ICollection<Supplier> AllSuppliers = new List<Supplier>();
        public static Product Product1 { get; } = new Product() { Name = "Product_1", };
        public static Product Product2 { get; } = new Product() { Name = "Product_2", };
        public static Supplier Supplier1 { get; } = new Supplier() { Name = "Supplier_1" };
        public static Supplier Supplier2 { get; } = new Supplier() { Name = "Supplier_2" };
        static TestData()
        {
            AllProducts.Add(Product1);
            AllProducts.Add(Product2);

            AllSuppliers.Add(Supplier1);
            AllSuppliers.Add(Supplier2);
        } 
    }
}
