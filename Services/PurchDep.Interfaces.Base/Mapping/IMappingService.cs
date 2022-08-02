namespace PurchDep.Interfaces.Base.Mapping
{
    public interface IMappingService<TSource, TResult> : IMapper<TSource, TResult>, IMapperAsync<TSource, TResult> where TSource : class where TResult : class
    {

    }
}
