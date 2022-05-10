namespace adAPI.Data.Repositories
{
    public interface IRepository<T>:IDisposable
    {
        IEnumerable<T> GetItems();
        T GetItemById(Guid itemId);
        void AddItem(T item);
        void Save();
    }
}
