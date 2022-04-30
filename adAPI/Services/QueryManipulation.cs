using adAPI.Contracts;
using adAPI.Models;

namespace adAPI
{
    public class QueryManipulation : IQueryManipulation<Advertisement, CollectionQueryParameters>
    {
        public IQueryable<Advertisement> SearchAdvertisements(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allAdvertisements)
        {
            return allAdvertisements.Where(x => x.Title.Contains(queryParameters.Search)
                || x.Description.Contains(queryParameters.Search));
        }

        public IQueryable<Advertisement> PagingAdvertisements(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allAdvertisements)
        {
            return allAdvertisements.OrderBy(on => on.Title)
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize);
        }

        public IQueryable<Advertisement> SortAdvertisements(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allAdvertisements)
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

        public Advertisement GetAdditionalFields(bool additionalFields, Advertisement advertisement)
        {
            if (additionalFields == true)
            {
                advertisement.IsDescriptionSerialize = true;
                advertisement.IsCreationDateSerialize = true;
            }

            return advertisement;
        }
    }
}
