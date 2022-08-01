namespace PurchDep.Dal.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public HashSet<Product> Products { get; set;} = new HashSet<Product>();
    }
}
