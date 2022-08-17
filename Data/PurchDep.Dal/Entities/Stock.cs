namespace PurchDep.Dal.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<StocksProduct> StocksProducts { get; set; } = new List<StocksProduct>();
    }
}
