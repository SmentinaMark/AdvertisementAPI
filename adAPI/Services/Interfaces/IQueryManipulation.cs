using adAPI.Data.Models;

namespace adAPI.Contracts
{
    /// <summary>
    /// Defines a contract that represents interaction with query parameters.
    /// </summary>
    public interface IQueryManipulation
    {
        /// <summary>
        /// The method allows to search in the collection.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <param name="allItems">List of elements.</param>
        /// <returns>Found elements.</returns>
        IQueryable<Advertisement> SearchItems(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allItems);

        /// <summary>
        ///  The method allows to paginate the collection.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <param name="allItems">List of elements.</param>
        /// <returns>Paginated elements.</returns>
        IQueryable<Advertisement> PagingItems(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allItems);

        /// <summary>
        /// The method allows to sort the collection.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <param name="allItems">List of elements.</param>
        /// <returns>Sorted elements.</returns>
        IQueryable<Advertisement> SortItems(CollectionQueryParameters queryParameters, IQueryable<Advertisement> allItems);

        /// <summary>
        ///  The method allows to include additional fields into the collection.
        /// </summary>
        /// <param name="additionalFields">Flag responsible for enabling additional fields.</param>
        /// <param name="item">List of elements.</param>
        /// <returns>An object with additional fields.</returns>
        Advertisement GetAdditionalFields(bool additionalFields, Advertisement item);
    }
}
