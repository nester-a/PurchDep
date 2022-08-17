namespace PurchDep.Domain
{
    /// <summary>Products that can be selled by supplier</summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public class SuppliersProduct<TKey> : Product<TKey>
    {
        /// <summary>Supplier's price for this product</summary>
        public decimal SuppliersPrice { get; set; }

        /// <summary>Id of the supplier selling this product</summary>
        public TKey SupplierId { get; set; } = default(TKey)!;
    }
    /// <summary>Products that can be selled by supplier with integer primary key</summary>
    public class SuppliersProduct : SuppliersProduct<int> { }
}
