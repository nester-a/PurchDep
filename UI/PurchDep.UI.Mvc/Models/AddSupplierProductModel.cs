using PurchDep.Domain;

namespace PurchDep.UI.Mvc.Models
{
    public class AddSupplierProductModel
    {
        public int? ProductId { get; set; }

        public bool NewProduct { get; set; }

        public int SupplierId { get; set; }

        public string NewProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
