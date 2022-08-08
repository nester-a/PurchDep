namespace PurchDep.Dal
{
    public class DbInitializer
    {
        public static void Initialize(PurchDepContext context)
        {
            context.Database.EnsureCreated();
            context.Products.AddRange(TestData.AllProducts);
            context.Suppliers.AddRange(TestData.AllSuppliers);
            context.SaveChanges();
        }
    }
}
