using PurchDep.Domain.Base.Core;

namespace PurchDep.Domain
{
    /// <summary>Simple product entity</summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public class Product<TKey> : IEntity<TKey>, INamedEntity<TKey>
    {
        /// <summary>Products Id</summary>
        public TKey Id { get; set; } = default(TKey)!;

        /// <summary>Products Name</summary>
        public string Name { get; set; } = string.Empty;
    }
    /// <summary>Simple product entity with integer primary key</summary>
    public class Product : Product<int>, IEntity, INamedEntity { }
}
