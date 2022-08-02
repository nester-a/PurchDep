using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchDep.Interfaces.Base.Services
{
    public class Service<T, TKey> : IService<T, TKey>, IAsyncService<T, TKey> where T : class
    {
        Repository<T, TKey> _repository;
        public Service(Repository<T, TKey> repository)
        {
            _repository = repository;
        }
        public T Add(T item)
        {
            try
            {
                _repository.Add(item);
            }
            catch
            {
                throw;
            }
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            try
            {
                var result = await _repository.AddAsync(item, cancel);
            }
            catch
            {
                throw;
            }
            return item;
        }

        public T Delete(TKey id)
        {
            T result;
            try
            {
                result = _repository.Delete(id);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<T> DeleteAsync(TKey id, CancellationToken cancel = default)
        {
            T result;
            try
            {
                result = await _repository.DeleteAsync(id, cancel);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public T Get(TKey id)
        {
            T result;
            try
            {
                result = _repository.Get(id);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public ICollection<T> GetAll()
        {
            ICollection<T> result;
            try
            {
                result = _repository.GetAll();
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<ICollection<T>> GetAllAsync(CancellationToken cancel = default)
        {
            ICollection<T> result;
            try
            {
                result = await _repository.GetAllAsync(cancel);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<T> GetAsync(TKey id, CancellationToken cancel = default)
        {
            T result;
            try
            {
                result = await _repository.GetAsync(id, cancel);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public T Update(TKey id, T updatedItem)
        {
            T result;
            try
            {
                result = _repository.Update(id, updatedItem);
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<T> UpdateAsync(TKey id, T updatedItem, CancellationToken cancel = default)
        {
            T result;
            try
            {
                result = await _repository.UpdateAsync(id, updatedItem, cancel);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
