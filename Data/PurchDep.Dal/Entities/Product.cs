﻿namespace PurchDep.Dal.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public HashSet<Supplier> Suppliers { get; set;} = new HashSet<Supplier>();
    }
}