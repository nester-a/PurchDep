namespace PurchDep.Dal.Entities
{
    public class SuppliersProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
