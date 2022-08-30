using PurchDep.Interfaces.Base.Mapping;

namespace PurchDep.Interfaces.Base.Services
{
    public abstract class Service<TSource, TResult, TKey> : IService<TResult, TKey> where TSource : class where TResult: class
    {
        protected Repository<TSource, TKey> Repository { get; private set; }
        protected IMappingService<TSource, TResult> Mapper { get; private set; }

        protected Service(Repository<TSource, TKey> repository, IMappingService<TSource, TResult> mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public abstract TResult Add(TResult item);

        public abstract Task<TResult> AddAsync(TResult item, CancellationToken cancel = default);

        public virtual TResult Delete(TKey id)
        {
            TSource sourceResult;
            try
            {
                sourceResult = Repository.Delete(id);
            }
            catch
            {
                throw;
            }

            var result = Mapper.Map(sourceResult);
            return result;
        }

        public virtual async Task<TResult> DeleteAsync(TKey id, CancellationToken cancel = default)
        {
            TSource sourceResult;
            try
            {
                sourceResult = await Repository.DeleteAsync(id, cancel);
            }
            catch
            {
                throw;
            }
            var result = await Mapper.MapAsync(sourceResult, cancel);
            return result;
        }

        public virtual TResult Get(TKey id)
        {
            TSource sourceResult;
            try
            {
                sourceResult = Repository.Get(id);
            }
            catch
            {
                throw;
            }
            var result = Mapper.Map(sourceResult);
            return result;
        }

        public virtual ICollection<TResult> GetAll()
        {
            ICollection<TSource> sourceResult;
            try
            {
                sourceResult = Repository.GetAll();
            }
            catch
            {
                throw;
            }
            var result = Mapper.MapRange(sourceResult);
            return result;
        }

        public virtual async Task<ICollection<TResult>> GetAllAsync(CancellationToken cancel = default)
        {
            ICollection<TSource> sourceResult;
            try
            {
                sourceResult = await Repository.GetAllAsync(cancel);
            }
            catch
            {
                throw;
            }
            var result = await Mapper.MapRangeAsync(sourceResult, cancel);
            return result;
        }

        public virtual async Task<TResult> GetAsync(TKey id, CancellationToken cancel = default)
        {
            TSource sourceResult;
            try
            {
                sourceResult = await Repository.GetAsync(id, cancel);
            }
            catch
            {
                throw;
            }
            var result = await Mapper.MapAsync(sourceResult, cancel);
            return result;
        }

        public virtual TResult Update(TKey id, TResult updatedItem)
        {
            TSource sourceResult;
            var sourceToUpdate = Mapper.Map(updatedItem);
            try
            {
                sourceResult = Repository.Update(id, sourceToUpdate);
            }
            catch
            {
                throw;
            }
            var result = Mapper.Map(sourceResult);
            return result;
        }

        public virtual async Task<TResult> UpdateAsync(TKey id, TResult updatedItem, CancellationToken cancel = default)
        {
            TSource sourceResult;
            var sourceToUpdate = await Mapper.MapAsync(updatedItem, cancel);
            try
            {
                sourceResult = await Repository.UpdateAsync(id, sourceToUpdate, cancel);
            }
            catch
            {
                throw;
            }
            var result = await Mapper.MapAsync(sourceResult, cancel);
            return result;
        }
    }
    public abstract class Service<TSource, TResult> : Service<TSource, TResult, int>, IService<TResult> where TSource : class where TResult : class
    {
        protected Service(Repository<TSource, int> repository, IMappingService<TSource, TResult> mapper) : base(repository, mapper) { }
    }
}
