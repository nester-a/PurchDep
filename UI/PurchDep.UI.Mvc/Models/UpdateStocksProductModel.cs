namespace PurchDep.UI.Mvc.Models
{
    public class UpdateStocksProductModel
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int StockId { get; set; }
        public int NewQuantity { get; set; }
    }
}
