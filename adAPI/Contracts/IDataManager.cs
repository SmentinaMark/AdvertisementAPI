namespace adAPI.Contracts
{
    /// <summary>
    /// Defines a contract that represents interaction with data.
    /// </summary>
    /// <typeparam name="T">Entity data type.</typeparam>
    /// <typeparam name="W">Parameters data type.</typeparam>
    public interface IDataManager<T, W>
    {
        /// <summary>
        /// The method allows you to get a list of items from the database context.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <returns>The final list of elements, edited depending on the passed parameters.</returns>
        List<T> GetItems(W queryParameters);

        /// <summary>
        /// The method allows you to get item by ID from the database context.
        /// </summary>
        /// <param name="id">ID of the element to be found in the database context.</param>
        /// <param name="additionalFields">Flag responsible for enabling additional fields.</param>
        /// <returns>Found object</returns>
        T GetItemById(Guid id, bool additionalFields);

        /// <summary>
        /// Allows you to add a new object to the database.
        /// </summary>
        /// <param name="newItem">The object to be added to the database.</param>
        /// <returns>Added object.</returns>
        T AddItem(T newItem);
    }
}
