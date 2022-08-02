using PurchDep.Domain.Base;

namespace PurchDep.Domain
{
    public class Supplier<T> : ISupplier<T>
    {
        public HashSet<IProduct<T>> Products { get; set; } = new();
        public T Id { get; set; } = default(T)!;
        public string Name { get; set; } = string.Empty;
    }
    public class Supplier : Supplier<int>, ISupplier { }
}
