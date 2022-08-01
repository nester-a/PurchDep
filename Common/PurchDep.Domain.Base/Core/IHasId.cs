namespace PurchDep.Domain.Base.Core
{
    public interface IHasId<T>
    {
        T Id { get; set; }
    }

    public interface IHasId : IHasId<int> { }
}
