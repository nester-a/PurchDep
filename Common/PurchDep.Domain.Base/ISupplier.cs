using PurchDep.Domain.Base.Core;

namespace PurchDep.Domain.Base
{
    public interface ISupplier : ICompany
    {
        HashSet<IProduct> Products { get; set; }
    }
}
