namespace PurchDep.UI.Mvc.Models
{
    public class UpdateSuppliersProductModel
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
