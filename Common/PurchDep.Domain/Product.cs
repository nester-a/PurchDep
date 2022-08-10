namespace PurchDep.Domain
{
    public class Product<T>
    {
        public T Id { get; set; } = default(T)!;
        public string Name { get; set; } = string.Empty;
    }
    public class Product : Product<int> { }
}
