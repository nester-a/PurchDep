namespace PurchDep.Interfaces.Base.Services
{
    public interface IAsyncRepository<T, in TKey> : IRepository<T, TKey> where T : class
    {
        Task<ICollection<T>> GetAllAsync(CancellationToken cancel = default);
        Task<T> GetAsync(TKey id, CancellationToken cancel = default);
        Task<T> AddAsync(T item, CancellationToken cancel = default);
        Task<T> UpdateAsync(TKey id, T updatedItem, CancellationToken cancel = default);
        Task<T> DeleteAsync(TKey id, CancellationToken cancel = default);
        Task SaveChangesAsync(CancellationToken cancel = default);
    }
    public interface IAsyncRepository<T> : IAsyncRepository<T, int>, IRepository<T> where T : class { }
}
