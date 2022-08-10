using PurchDep.Dal.Entities;
using System.Collections.Generic;

namespace Services.PurchDep.Interfaces.Tests.Data
{
    public static class RepoTestData
    {
        public static ICollection<Product> AllProducts = new List<Product>();
        public static ICollection<Supplier> AllSuppliers = new List<Supplier>();
        public static Product Product1 { get; } = new Product() { Name = "Product_1", };
        public static Product Product2 { get; } = new Product() { Name = "Product_2", };
        public static Product Product3 { get; } = new Product() { Name = "Product_3", };
        public static Product Product4 { get; } = new Product() { Name = "Product_4", };
        public static Product Product5 { get; } = new Product() { Name = "Product_5", };
        public static Product Product6 { get; } = new Product() { Name = "Product_6", };
        public static Supplier Supplier1 { get; } = new Supplier() { Name = "Supplier_1" };
        public static Supplier Supplier2 { get; } = new Supplier() { Name = "Supplier_2" };
        public static Supplier Supplier3 { get; } = new Supplier() { Name = "Supplier_3" };
        public static Supplier Supplier4 { get; } = new Supplier() { Name = "Supplier_4" };
        public static Supplier Supplier5 { get; } = new Supplier() { Name = "Supplier_5" };
        public static Supplier Supplier6 { get; } = new Supplier() { Name = "Supplier_6" };
        static RepoTestData()
        {
            AllProducts.Add(Product1);
            AllProducts.Add(Product2);

            AllSuppliers.Add(Supplier1);
            AllSuppliers.Add(Supplier2);
        }
    }
}
