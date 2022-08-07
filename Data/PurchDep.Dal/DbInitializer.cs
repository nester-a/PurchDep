namespace PurchDep.Dal
{
    public class DbInitializer
    {
        public static void Initialize(PurchDepContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
