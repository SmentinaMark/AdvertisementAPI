using adAPI.Contracts;
using adAPI.Data.Models;

namespace adAPI
{
    public class QueryManipulation : IQueryManipulation
    {
        /// <summary>
        /// The method allows to search items.
        /// </summary>
        /// <param name="queryParameters">Parameter for seacrh.</param>
        /// <param name="allAdvertisements">List of items.</param>
        /// <returns>Found elements.</returns>
        public IQueryable<Advertisement> SearchItems(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allAdvertisements)
        {
            return allAdvertisements.Where(x => x.Title.Contains(queryParameters.Search)
                || x.Description.Contains(queryParameters.Search));
        }

        /// <summary>
        /// The method allows to paginage items.
        /// </summary>
        /// <param name="queryParameters">Parameter for paginate.</param>
        /// <param name="allAdvertisements">List of items.</param>
        /// <returns>Paginated eleьents.</returns>
        public IQueryable<Advertisement> PagingItems(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allAdvertisements)
        {
            return allAdvertisements.OrderBy(on => on.Title)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize);
        }

        /// <summary>
        /// The method allows to sort items.
        /// </summary>
        /// <param name="queryParameters">Parameter for sorting.</param>
        /// <param name="allAdvertisements">List of items.</param>
        /// <returns>Sorted list.</returns>
        public IQueryable<Advertisement> SortItems(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allAdvertisements)
        {
            IQueryable<Advertisement> sortedMeetup = queryParameters.Sort switch
            {
                "price_asc" => allAdvertisements.OrderBy(s => s.Price),
                "price_desc" => allAdvertisements.OrderByDescending(s => s.Price),
                "date_asc" => allAdvertisements.OrderBy(s => s.CreationDate),
                "date_desc" => allAdvertisements.OrderByDescending(s => s.CreationDate),
                _ => allAdvertisements.OrderBy(s => s.Title)
            };

            return sortedMeetup;
        }

        /// <summary>
        /// The method allows to get additional fields.
        /// </summary>
        /// <param name="additionalFields">Flag.</param>
        /// <param name="advertisement">Object to be returned.</param>
        /// <returns>Object with additional fields.</returns>
        public Advertisement GetAdditionalFields(bool additionalFields, Advertisement advertisement)
        {
            if(advertisement == null)
            {
                return null;
            }
            if (additionalFields)
            {
                advertisement.IsDescriptionSerialize = true;
                advertisement.IsCreationDateSerialize = true;
                advertisement.IsGetAllImages = true;
            }

            return advertisement;
        }
    }
}
