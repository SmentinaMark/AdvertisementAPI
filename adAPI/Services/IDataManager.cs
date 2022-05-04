using adAPI.Models;

namespace adAPI.Contracts
{
    /// <summary>
    /// Defines a contract that represents interaction with data.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// The method allows to get a list of items from the database context.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <returns>The final list of elements, edited depending on the passed parameters.</returns>
        List<Advertisement> GetItems(CollectionQueryParameters queryParameters);

        /// <summary>
        /// The method allows to get item by ID from the database context.
        /// </summary>
        /// <param name="id">ID of the element to be found in the database context.</param>
        /// <param name="additionalFields">Flag responsible for enabling additional fields.</param>
        /// <returns>Found object</returns>
        Advertisement GetItemById(Guid id, bool additionalFields);

        /// <summary>
        /// Allows to add a new object to the database.
        /// </summary>
        /// <param name="newItem">The object to be added to the database.</param>
        /// <returns>Added object.</returns>
        Advertisement AddItem(Advertisement newItem);
    }
}
