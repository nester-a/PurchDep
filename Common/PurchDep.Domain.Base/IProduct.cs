using PurchDep.Domain.Base.Core;

namespace PurchDep.Domain.Base
{
    public interface IProduct<T> : IHasId<T>, IHasName, IHasPrice { }
    public interface IProduct : IProduct<int>, IHasId, IHasName, IHasPrice { }
}
