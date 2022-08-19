namespace PurchDep.Domain
{
    /// <summary>Supplier enity</summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public class Supplier<TKey>
    {
        /// <summary>Suppliers Id</summary>
        public TKey Id { get; set; } = default(TKey)!;

        /// <summary>Suppliers Name</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Suppliers products that can be selled</summary>
        public HashSet<SuppliersProduct<TKey>> SuppliersProducts { get; set; } = new();
    }
    /// <summary>Supplier enity with integer primary key</summary>
    public class Supplier : Supplier<int>
    {
        public new HashSet<SuppliersProduct> SuppliersProducts { get; set; } = new();
    }
}
