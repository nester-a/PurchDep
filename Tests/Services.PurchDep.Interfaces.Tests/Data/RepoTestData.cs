﻿using PurchDep.Dal.Entities;
using System.Collections.Generic;

namespace Services.PurchDep.Interfaces.Tests.Data
{
    public static class RepoTestData
    {
        public static ICollection<Product> AllProducts = new List<Product>();
        public static ICollection<Supplier> AllSuppliers = new List<Supplier>();
        public static Product Product1 { get; } = new Product() { Name = "Product_1", Price = 1.11m, };
        public static Product Product2 { get; } = new Product() { Name = "Product_2", Price = 2.22m, };
        public static Product Product3 { get; } = new Product() { Name = "Product_3", Price = 3.33m, };
        public static Product Product4 { get; } = new Product() { Name = "Product_4", Price = 4.44m, };
        public static Product Product5 { get; } = new Product() { Name = "Product_5", Price = 5.55m, };
        public static Product Product6 { get; } = new Product() { Name = "Product_6", Price = 6.66m, };
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

            Supplier1.Products.Add(Product1);
            Supplier2.Products.Add(Product2);
            Supplier3.Products.Add(Product3);
            Supplier4.Products.Add(Product4);
            Supplier5.Products.Add(Product5);
            Supplier6.Products.Add(Product6);

            AllSuppliers.Add(Supplier1);
            AllSuppliers.Add(Supplier2);
        }
    }
}