using adAPI.Contracts;
using adAPI.Data;
using adAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace adAPI.Services
{
    public class DataManager : IDataManager<Advertisement>
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryManipulation<Advertisement, CollectionQueryParameters> _queryManipulation;

        public DataManager(ApplicationDbContext context , IQueryManipulation<Advertisement, CollectionQueryParameters> queryManipulation)
        {
            _context = context;
            _queryManipulation = queryManipulation;
        }

        public async Task<IEnumerable<Advertisement>> GetItemsAsync(CollectionQueryParameters queryParameters)
        {
            var allAdvertisements = _context.Advertisements.AsQueryable();

            if (string.IsNullOrWhiteSpace(queryParameters.Search))
            {
                allAdvertisements = _queryManipulation.PagingAdvertisements(queryParameters, allAdvertisements);
            }
            else
            {
                allAdvertisements = _queryManipulation.SearchAdvertisements(queryParameters, allAdvertisements);
            }
            allAdvertisements = _queryManipulation.SortAdvertisements(queryParameters, allAdvertisements);

            return await allAdvertisements.ToListAsync();
        }

        public Advertisement GetItemById(Guid id, bool additionalFields)
        {
            var advertisement = _context.Advertisements.Find(id);
            advertisement = _queryManipulation.GetAdditionalFields(additionalFields, advertisement);

            return advertisement;
        }

        public async Task<Advertisement> AddItemAsync(Advertisement newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.CreationDate = DateTime.Now.Date;
            _context.Advertisements.Add(newItem);
            await _context.SaveChangesAsync();

            return newItem;
        }
    }
}
