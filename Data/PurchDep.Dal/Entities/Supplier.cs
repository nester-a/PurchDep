namespace PurchDep.Dal.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<SuppliersProduct> SuppliersProducts { get; set;} = new List<SuppliersProduct>();
    }
}
