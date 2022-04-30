using adAPI.Models;

namespace adAPI.Contracts
{
    public interface IDataManager<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(CollectionQueryParameters queryParameters);
        T GetItemById(Guid id, bool additionalFields);
        Task<T> AddItemAsync(T newItem);
    }
}
