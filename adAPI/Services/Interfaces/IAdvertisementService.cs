using adAPI.Contracts.Requests;
using adAPI.Data.Models;

namespace adAPI.Services
{
    public interface IAdvertisementService
    {
        List<Advertisement> GetItems(CollectionQueryParameters queryParameters);
        Advertisement GetItemById(Guid id, bool additionalFields);
        Advertisement AddItem(Advertisement newItem);
    }
}
