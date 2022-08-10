namespace PurchDep.Domain
{
    public class Stock<T>
    {
        public T Id { get; set; } = default(T)!;
        public string Name { get; set; } = string.Empty;
        public HashSet<StocksProduct<T>> StocksProducts { get; set; } = new();
    }
    public class Stock : Stock<int> { }
}
