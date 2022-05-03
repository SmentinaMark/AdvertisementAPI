using adAPI.Contracts;
using adAPI.Data;
using adAPI.Models;

namespace adAPI.Services
{
    public class DataManager : IDataManager<Advertisement, CollectionQueryParameters>
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryManipulation<Advertisement, CollectionQueryParameters> _queryManipulation;

        public DataManager(ApplicationDbContext context, IQueryManipulation<Advertisement, CollectionQueryParameters> queryManipulation)
        {
            _context = context;
            _queryManipulation = queryManipulation;
        }

        public List<Advertisement> GetItems(CollectionQueryParameters queryParameters)
        {
            var allAdvertisements = _context.Advertisements.AsQueryable();

            if (allAdvertisements != null)
            {
                if (string.IsNullOrWhiteSpace(queryParameters.Search))
                {
                    allAdvertisements = _queryManipulation.PagingItems(queryParameters, allAdvertisements);
                }
                else
                {
                    allAdvertisements = _queryManipulation.SearchItems(queryParameters, allAdvertisements);
                }
                allAdvertisements = _queryManipulation.SortItems(queryParameters, allAdvertisements);
            }

            return allAdvertisements.ToList();
        }

        public Advertisement GetItemById(Guid id, bool additionalFields)
        {
            var advertisement = _context.Advertisements.Find(id);
            advertisement = _queryManipulation.GetAdditionalFields(additionalFields, advertisement);

            return advertisement;
        }

        public Advertisement AddItem(Advertisement newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.CreationDate = DateTime.Now.Date;

            _context.Advertisements.Add(newItem);
            _context.SaveChanges();

            return newItem;
        }
    }
}
