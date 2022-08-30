using PurchDep.Domain.Base.Core;

namespace PurchDep.Domain
{
    /// <summary>Stock entity</summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public class Stock<TKey> : IEntity<TKey>, INamedEntity<TKey>
    {
        /// <summary>Stocks Id</summary>
        public TKey Id { get; set; } = default(TKey)!;

        /// <summary>Stocks Name</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Products in stock</summary>
        public HashSet<StocksProduct<TKey>> StocksProducts { get; set; } = new();
    }
    /// <summary>Stock entity with integer primary key</summary>
    public class Stock : Stock<int>,  IEntity, INamedEntity
    {
        public new HashSet<StocksProduct> StocksProducts { get; set; } = new();
    }
}
