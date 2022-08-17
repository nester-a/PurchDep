﻿namespace PurchDep.Dal.Entities
{
    public class StocksProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public int StockId { get; set; }
        public Stock Stock { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
