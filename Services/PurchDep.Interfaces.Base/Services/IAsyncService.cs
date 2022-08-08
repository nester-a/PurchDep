namespace PurchDep.Interfaces.Base.Services
{
    public interface IAsyncService<T, in TKey> : ISyncService<T, TKey> where T : class
    {
        Task<ICollection<T>> GetAllAsync(CancellationToken cancel = default);
        Task<T> GetAsync(TKey id, CancellationToken cancel = default);
        Task<T> AddAsync(T item, CancellationToken cancel = default);
        Task<T> UpdateAsync(TKey id, T updatedItem, CancellationToken cancel = default);
        Task<T> DeleteAsync(TKey id, CancellationToken cancel = default);
    }
    public interface IAsyncService<T> : IAsyncService<T, int>, ISyncService<T> where T : class { }
}
