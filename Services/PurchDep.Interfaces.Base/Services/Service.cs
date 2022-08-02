using PurchDep.Interfaces.Base.Mapping;

namespace PurchDep.Interfaces.Base.Services
{
    public class Service<TSource, TResult, TKey> : IService<TResult, TKey>, IAsyncService<TResult, TKey> where TSource : class where TResult: class
    {
        Repository<TSource, TKey> _repository;
        IMappingService<TSource, TResult> _mapper;

        public Service(Repository<TSource, TKey> repository, IMappingService<TSource, TResult> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public TResult Add(TResult item)
        {
            var itemToAdd = _mapper.Map(item);
            try
            {
                _repository.Add(itemToAdd);
            }
            catch
            {
                throw;
            }
            return item;
        }

        public async Task<TResult> AddAsync(TResult item, CancellationToken cancel = default)
        {
            var itemToAdd = await _mapper.MapAsync(item, cancel);
            try
            {
                var result = await _repository.AddAsync(itemToAdd, cancel);
            }
            catch
            {
                throw;
            }
            return item;
        }

        public TResult Delete(TKey id)
        {
            TSource sourceResult;
            try
            {
                sourceResult = _repository.Delete(id);
            }
            catch
            {
                throw;
            }

            var result = _mapper.Map(sourceResult);
            return result;
        }

        public async Task<TResult> DeleteAsync(TKey id, CancellationToken cancel = default)
        {
            TSource sourceResult;
            try
            {
                sourceResult = await _repository.DeleteAsync(id, cancel);
            }
            catch
            {
                throw;
            }
            var result = await _mapper.MapAsync(sourceResult, cancel);
            return result;
        }

        public TResult Get(TKey id)
        {
            TSource sourceResult;
            try
            {
                sourceResult = _repository.Get(id);
            }
            catch
            {
                throw;
            }
            var result = _mapper.Map(sourceResult);
            return result;
        }

        public ICollection<TResult> GetAll()
        {
            ICollection<TSource> sourceResult;
            try
            {
                sourceResult = _repository.GetAll();
            }
            catch
            {
                throw;
            }
            var result = _mapper.MapRange(sourceResult);
            return result;
        }

        public async Task<ICollection<TResult>> GetAllAsync(CancellationToken cancel = default)
        {
            ICollection<TSource> sourceResult;
            try
            {
                sourceResult = await _repository.GetAllAsync(cancel);
            }
            catch
            {
                throw;
            }
            var result = await _mapper.MapRangeAsync(sourceResult, cancel);
            return result;
        }

        public async Task<TResult> GetAsync(TKey id, CancellationToken cancel = default)
        {
            TSource sourceResult;
            try
            {
                sourceResult = await _repository.GetAsync(id, cancel);
            }
            catch
            {
                throw;
            }
            var result = await _mapper.MapAsync(sourceResult, cancel);
            return result;
        }

        public TResult Update(TKey id, TResult updatedItem)
        {
            TSource sourceResult;
            var sourceToUpdate = _mapper.Map(updatedItem);
            try
            {
                sourceResult = _repository.Update(id, sourceToUpdate);
            }
            catch
            {
                throw;
            }
            var result = _mapper.Map(sourceResult);
            return result;
        }

        public async Task<TResult> UpdateAsync(TKey id, TResult updatedItem, CancellationToken cancel = default)
        {
            TSource sourceResult;
            var sourceToUpdate = await _mapper.MapAsync(updatedItem, cancel);
            try
            {
                sourceResult = await _repository.UpdateAsync(id, sourceToUpdate, cancel);
            }
            catch
            {
                throw;
            }
            var result = await _mapper.MapAsync(sourceResult, cancel);
            return result;
        }
    }
}
