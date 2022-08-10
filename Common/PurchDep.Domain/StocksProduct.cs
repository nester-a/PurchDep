namespace PurchDep.Domain
{
    /// <summary>Purchased product that may be in stock</summary>
    /// <typeparam name="TKey"></typeparam>
    public class StocksProduct<TKey> : SuppliersProduct<TKey>
    {
        /// <summary>Products quantity</summary>
        public int Quantity { get; set; }
    }
    /// <summary>Purchased product that may be in stock with integer primary key</summary>
    public class StocksProduct : StocksProduct<int> { }
}
