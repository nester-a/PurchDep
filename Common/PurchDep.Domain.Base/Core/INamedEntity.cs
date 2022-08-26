namespace PurchDep.Domain.Base.Core
{
    public interface INamedEntity<TKey> : IEntity<TKey>
    {
        string Name { get; set; }
    }
    public interface INamedEntity : INamedEntity<int>, IEntity { }
}
