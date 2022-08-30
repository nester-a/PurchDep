namespace PurchDep.Interfaces.Base.Services
{
    public interface IService<TResult, TKey> : ISyncService<TResult, TKey>, IAsyncService<TResult, TKey> where TResult : class { }
    public interface IService<TResult> : IService<TResult, int>, ISyncService<TResult>, IAsyncService<TResult> where TResult : class { }
}
