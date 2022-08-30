namespace PurchDep.Interfaces.Base.Mapping
{
    public interface IMapperAsync<TSource, TResult> : IMapper<TSource, TResult> where TSource : class where TResult : class
    {
        Task<TResult> MapAsync(TSource item, CancellationToken cancel = default);
        Task<ICollection<TResult>> MapRangeAsync(ICollection<TSource> items, CancellationToken cancel = default);
        Task<TSource> MapAsync(TResult item, CancellationToken cancel = default);
        Task<ICollection<TSource>> MapRangeAsync(ICollection<TResult> items, CancellationToken cancel = default);
    }
}
