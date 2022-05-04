using adAPI.Contracts;
using adAPI.Data;
using adAPI.Models;

namespace adAPI.Services
{
    public class DataManager : IDataManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryManipulation _queryManipulation;

        public DataManager(ApplicationDbContext context, IQueryManipulation queryManipulation)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _queryManipulation = queryManipulation ?? throw new ArgumentNullException(nameof(queryManipulation));
        }

        /// <summary>
        /// The method allows to get the final list of advertisements.
        /// </summary>
        /// <param name="queryParameters">Object with query parameters for view finished collection.</param>
        /// <returns>List of advertisements.</returns>
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

        /// <summary>
        /// Allows to get item from context database by Id.
        /// </summary>
        /// <param name="id">Шd of the required element.</param>
        /// <param name="additionalFields">flag for getting additional fields.</param>
        /// <returns>Found object.</returns>
        public Advertisement GetItemById(Guid id, bool additionalFields)
        {
            var advertisement = _context.Advertisements.Find(id);

            if(additionalFields)
            {
                advertisement = _queryManipulation.GetAdditionalFields(additionalFields, advertisement);
            }

            return advertisement;
        }

        /// <summary>
        /// Allows to Add new Item.
        /// </summary>
        /// <param name="newItem">Item for adding into database context.</param>
        /// <returns>Created item.</returns>
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
