namespace PurchDep.Interfaces.Base.Mapping
{
    public interface IMapper<TSource, TResult> where TSource : class where TResult : class
    {
        TResult Map(TSource item);
        ICollection<TResult> MapRange(ICollection<TSource> items);
        TSource Map(TResult item);
        ICollection<TSource> MapRange(ICollection<TResult> items);
    }
}
