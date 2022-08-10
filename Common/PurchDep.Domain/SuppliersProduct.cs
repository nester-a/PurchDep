namespace PurchDep.Domain
{
    public class SuppliersProduct<T> : Product<T>
    {
        public decimal SupplierPrice { get; set; }
    }
    public class SuppliersProduct : SuppliersProduct<int> { }
}
