using PurchDep.Domain.Base.Core;

namespace PurchDep.Domain.Base
{
    public interface ISupplier<T> : ICompany<T>
    {
        HashSet<IProduct<T>> Products { get; set; }
    }
    public interface ISupplier : ISupplier<int>, ICompany { }
}
