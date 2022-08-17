using PurchDep.Dal.Entities;

namespace PurchDep.Dal
{
    public static class TestData
    {
        public static ICollection<Product> AllProducts = new List<Product>();
        public static ICollection<Supplier> AllSuppliers = new List<Supplier>();
        public static ICollection<Stock> AllStocks = new List<Stock>();
        public static Product Product1 { get; } = new() { Name = "Product_1", };
        public static Product Product2 { get; } = new() { Name = "Product_2", };
        public static Supplier Supplier1 { get; } = new() { Name = "Supplier_1" };
        public static Supplier Supplier2 { get; } = new() { Name = "Supplier_2" };

        public static SuppliersProduct SuppliersProduct1 { get; } = new()
        {
            Price = 10m,
            Product = Product1,
            ProductId = Product1.Id,
            Supplier = Supplier1,
            SupplierId = Supplier1.Id,
        };
        public static SuppliersProduct SuppliersProduct2 { get; } = new()
        {
            Price = 10m,
            Product = Product2,
            ProductId = Product2.Id,
            Supplier = Supplier2,
            SupplierId = Supplier2.Id,
        };
        public static Stock Stock1 { get; } = new() { Name = "Stock_1" };
        public static Stock Stock2 { get; } = new() { Name = "Stock_2" };

        public static StocksProduct StocksProduct1 { get; } = new()
        {
            Product = Product1,
            ProductId = Product1.Id,
            Supplier = Supplier1,
            SupplierId = Supplier1.Id,
            Stock = Stock1,
            StockId = Stock1.Id,
            Quantity = 10,
        };
        public static StocksProduct StocksProduct2 { get; } = new()
        {
            Product = Product2,
            ProductId = Product2.Id,
            Supplier = Supplier2,
            SupplierId = Supplier2.Id,
            Stock = Stock2,
            StockId = Stock2.Id,
            Quantity = 10,
        };
        static TestData()
        {
            AllProducts.Add(Product1);
            AllProducts.Add(Product2);

            AllSuppliers.Add(Supplier1);
            AllSuppliers.Add(Supplier2);

            AllStocks.Add(Stock1);
            AllStocks.Add(Stock2);

            Supplier1.SuppliersProducts.Add(SuppliersProduct1);
            Supplier2.SuppliersProducts.Add(SuppliersProduct2);

            Stock1.StocksProducts.Add(StocksProduct1);
            Stock2.StocksProducts.Add(StocksProduct2);
        }
    }
}
