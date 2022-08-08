using PurchDep.Dal.Entities;

namespace PurchDep.Dal
{
    public static class TestData
    {
        public static ICollection<Product> AllProducts = new List<Product>();
        public static ICollection<Supplier> AllSuppliers = new List<Supplier>();
        public static Product Product1 { get; } = new Product() { Name = "Product_1", Price = 1.11m, };
        public static Product Product2 { get; } = new Product() { Name = "Product_2", Price = 2.22m, };
        public static Supplier Supplier1 { get; } = new Supplier() { Name = "Supplier_1" };
        public static Supplier Supplier2 { get; } = new Supplier() { Name = "Supplier_2" };
        static TestData()
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
