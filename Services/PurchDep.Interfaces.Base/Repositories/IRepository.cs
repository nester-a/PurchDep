namespace PurchDep.Interfaces.Base.Services
{
    public interface IRepository<T, in TKey> where T : class
    {
        ICollection<T> GetAll();
        T Get(TKey id);
        T Add(T item);
        T Update(TKey id, T updatedItem);
        T Delete(TKey id);
        void SaveChanges();
    }
    public interface IRepository<T>: IRepository<T, int> where T : class { }
}
