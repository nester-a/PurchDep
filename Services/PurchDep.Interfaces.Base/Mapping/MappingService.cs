namespace PurchDep.Interfaces.Base.Mapping
{
    public abstract class MappingService<TSource, TResult> : IMappingService<TSource, TResult> where TSource : class where TResult : class
    {
        public abstract TResult Map(TSource item);

        public abstract TSource Map(TResult item);

        public async virtual Task<TResult> MapAsync(TSource item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var itemTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await itemTask;
        }

        public async virtual Task<TSource> MapAsync(TResult item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var itemTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await itemTask;
        }

        public virtual ICollection<TResult> MapRange(ICollection<TSource> items)
        {
            if (items is null) return null!;
            var result = new List<TResult>();
            foreach (var item in items)
            {
                result.Add(Map(item));
            }
            return result;
        }

        public virtual ICollection<TSource> MapRange(ICollection<TResult> items)
        {
            if (items is null) return null!;
            var result = new List<TSource>();
            foreach (var item in items)
            {
                result.Add(Map(item));
            }
            return result;
        }

        public async virtual Task<ICollection<TResult>> MapRangeAsync(ICollection<TSource> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var itemsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await itemsTask;
        }

        public async virtual Task<ICollection<TSource>> MapRangeAsync(ICollection<TResult> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var itemsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await itemsTask;
        }
    }
}
