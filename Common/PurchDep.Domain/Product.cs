namespace PurchDep.Domain
{
    public class Product<T>/* : IProduct<T>*/
    {
        public T Id { get; set; } = default(T)!;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
    public class Product : Product<int>/*, IProduct */{ }
}
