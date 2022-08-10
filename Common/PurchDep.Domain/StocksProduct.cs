namespace PurchDep.Domain
{
    public class StocksProduct<T> : SuppliersProduct<T>
    {
        public int Quantity { get; set; }
    }
    public class StocksProduct : StocksProduct<int> { }
}
