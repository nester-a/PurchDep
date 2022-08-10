namespace PurchDep.Domain
{
    public class Supplier<T>
    {
        public T Id { get; set; } = default(T)!;
        public string Name { get; set; } = string.Empty;
        public HashSet<Product<T>> Products { get; set; } = new();
        public HashSet<SuppliersProduct<T>> SuppliersProducts { get; set; } = new();
    }
    public class Supplier : Supplier<int> { }
}
