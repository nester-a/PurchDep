namespace PurchDep.Domain.Base.Core
{
    public interface ICompany<T> : IHasId<T>, IHasName { }
    public interface ICompany : ICompany<int>, IHasId, IHasName { }
}
