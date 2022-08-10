namespace PurchDep.Domain
{
    /// <summary>Purchased product that may be in stock</summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public class StocksProduct<TKey> : SuppliersProduct<TKey>
    {
        /// <summary>Products quantity</summary>
        public int Quantity { get; set; }

        /// <summary>The Id of the supplier from whom this product was purchased</summary>
        public TKey SupplierId { get; set; } = default(TKey)!;

        /// <summary>The Id of the stock where this product is stored</summary>
        public TKey StockId { get; set; } = default(TKey)!;
    }
    /// <summary>Purchased product that may be in stock with integer primary key</summary>
    public class StocksProduct : StocksProduct<int> { }
}
