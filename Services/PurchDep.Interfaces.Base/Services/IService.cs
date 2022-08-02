namespace PurchDep.Interfaces.Base.Services
{
    public interface IService<T, in TKey> where T: class
    {
        ICollection<T> GetAll();
        T Get(TKey id);
        T Add(T item);
        T Update(TKey id, T updatedItem);
        T Delete(TKey id);
    }

    public interface IService<T> : IService<T, int> where T : class { }
}
