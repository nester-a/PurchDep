using PurchDep.Interfaces.Base.Services;
using System.Net;
using System.Net.Http.Json;

namespace PurchDep.Interfaces.Base.Web
{
    public abstract class ClientBase<T> : IService<T>, IDisposable where T : class
    {
        bool _disposed;
        protected HttpClient Client { get; }
        protected string Address { get; }

        protected ClientBase(HttpClient client, string address)
        {
            Client = client;
            Address = address;
        }
        public void Dispose()
        {
            if (_disposed) return;
            Dispose(true);
            _disposed = true;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //освободить все управляемые ресурсы (удалить всё, что было создано в этом объекте)
            }

            //освобождение всех неуправляемых
        }
        private async Task<T> GetAsync(string url, CancellationToken cancel = default)
        {
            var response = await Client.GetAsync(url, cancel);

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                if(ex is not null) throw new ArgumentException($"Request - {url}. Response - NOT FOUND");
            }

            var result = await response
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);

            return result!;
        }
        private T Get(string url) => GetAsync(url).Result;
        private async Task<ICollection<T>> GetAllAsync(string url, CancellationToken cancel = default)
        {
            var response = await Client.GetAsync(url, cancel).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return new List<T>();
            }

            var result = await response
                .Content
                .ReadFromJsonAsync<ICollection<T>>(cancellationToken: cancel)
                .ConfigureAwait(false);

            return result!;
        }
        private ICollection<T> GetAll(string url) => GetAllAsync(url).Result;
        private async Task<HttpResponseMessage> PostAsync(string url, T value, CancellationToken cancel = default)
        {
            var response = await Client.PostAsJsonAsync(url, value, cancel).ConfigureAwait(false);

            return response;
        }
        private HttpResponseMessage Post(string url, T value) => PostAsync(url, value).Result;
        private async Task<HttpResponseMessage> PutAsync(string url, T value, CancellationToken cancel = default)
        {
            var response = await Client.PutAsJsonAsync(url, value, cancel).ConfigureAwait(false);

            return response;
        }
        private HttpResponseMessage Put(string url, T value) => PutAsync(url, value).Result;
        private async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancel = default)
        {
            var response = await Client.DeleteAsync(url, cancel).ConfigureAwait(false);

            return response;
        }
        private HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        public async Task<ICollection<T>> GetAllAsync(CancellationToken cancel = default)
        {
            var items = await GetAllAsync(Address);
            return items;
        }

        public async Task<T> GetAsync(int id, CancellationToken cancel = default)
        {
            T item;
            try
            {
                item = await GetAsync($"{Address}/{id}", cancel);
            }
            catch
            {
                throw;
            }
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException("The Item being added is null");

            var response = await PostAsync(Address, item, cancel);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"This Item-{nameof(item)} cannot be added");
            }
            var addedItem = await response.Content.ReadFromJsonAsync<T>();

            return addedItem!;
        }

        public async Task<T> UpdateAsync(int id, T updatedItem, CancellationToken cancel = default)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null");

            var response = await PutAsync($"{Address}/{id}", updatedItem, cancel);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"This Item-{nameof(updatedItem)} cannot be updated");
            }
            var resultItem = await response.Content.ReadFromJsonAsync<T>();

            return resultItem!;
        }

        public async Task<T> DeleteAsync(int id, CancellationToken cancel = default)
        {
            var response = await DeleteAsync($"{Address}/{id}", cancel);
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"This Item with Id-{id} cannot be deleted");
            }
            var deletedItem = await response.Content.ReadFromJsonAsync<T>();
            return deletedItem!;
        }

        public ICollection<T> GetAll()
        {
            var items = GetAll(Address);
            return items;
        }

        public T Get(int id)
        {
            T item;
            try
            {
                item = Get($"{Address}/{id}");
            }
            catch
            {
                throw;
            }
            return item;
        }

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException("The Item being added is null");

            var response = Post(Address, item);
            if (!response.IsSuccessStatusCode)
            {
                    throw new InvalidOperationException($"This Item-{nameof(item)} cannot be added");
            }
            var addedItem = response.Content.ReadFromJsonAsync<T>().Result;

            return addedItem!;
        }

        public T Update(int id, T updatedItem)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null");

            var response = Put($"{Address}/{id}", updatedItem);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"This Item-{nameof(updatedItem)} cannot be updated");
            }
            var resultItem = response.Content.ReadFromJsonAsync<T>().Result;

            return resultItem!;
        }

        public T Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"This Item with Id-{id} cannot be deleted");
            }
            var deletedItem = response.Content.ReadFromJsonAsync<T>().Result;
            return deletedItem!;
        }
    }
}
