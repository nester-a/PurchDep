using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;

namespace PurchDep.Interfaces.Base.Services
{
    public abstract class Repository<T, TKey> : IRepository<T, TKey>, IAsyncRepository<T, TKey> where T : class
    {
        private readonly PurchDepContext _context;
        protected DbSet<T> Set { get; }
        protected virtual IQueryable<T> Items => Set;
        public Repository(PurchDepContext context)
        {
            _context = context;
            Set = context.Set<T>();
        }
        public virtual T Add(T item)
        {
            if(item is null) throw new ArgumentNullException("The Item being added is null");
            var checkResult = Items.Contains(item);
            if (checkResult) throw new ArgumentException("The Item being added is already available in the database");

            Set.Add(item);
            SaveChanges();

            return item;
        }

        public virtual async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException("The Item being added is null");
            var checkResult = await Items.ContainsAsync(item, cancel);
            if (checkResult) throw new ArgumentException("The Item being added is already available in the database");

            await Set.AddAsync(item, cancel);
            await SaveChangesAsync();

            return item;
        }

        public abstract T Delete(TKey id);

        public abstract Task<T> DeleteAsync(TKey id, CancellationToken cancel = default);

        public abstract T Get(TKey id);

        public virtual ICollection<T> GetAll()
        {
            var query = Items;
            if (query is null) return (ICollection<T>)Enumerable.Empty<T>();

            var result = query.ToArray();
            return result;
        }

        public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken cancel = default)
        {
            var query = Items;
            if (query is null) return (ICollection<T>)Enumerable.Empty<T>();

            var result = await query.ToArrayAsync(cancel).ConfigureAwait(false);
            return result;
        }

        public abstract Task<T> GetAsync(TKey id, CancellationToken cancel = default);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancel = default)
        {
            await _context.SaveChangesAsync(cancel);
        }

        public abstract T Update(TKey id, T updatedItem);

        public abstract Task<T> UpdateAsync(TKey id, T updatedItem, CancellationToken cancel = default);
    }

    public abstract class Repository<T> : Repository<T, int>, IRepository<T>, IAsyncRepository<T> where T : class
    {
        protected Repository(PurchDepContext context) : base(context) { }
    }
}
