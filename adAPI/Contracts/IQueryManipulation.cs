namespace adAPI.Contracts
{
    /// <summary>
    /// Defines a contract that represents interaction with query parameters.
    /// </summary>
    /// <typeparam name="T">Entity data type.</typeparam>
    /// <typeparam name="W">Parameters data type.</typeparam>
    public interface IQueryManipulation<T, W>
    {
        /// <summary>
        /// The method allows to search in the collection.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <param name="allItems">List of elements.</param>
        /// <returns>Found elements.</returns>
        IQueryable<T> SearchItems(W queryParameters, IQueryable<T> allItems);

        /// <summary>
        ///  The method allows to paginate the collection.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <param name="allItems">List of elements.</param>
        /// <returns>Paginated elements.</returns>
        IQueryable<T> PagingItems(W queryParameters, IQueryable<T> allItems);

        /// <summary>
        /// The method allows to sort the collection.
        /// </summary>
        /// <param name="queryParameters">An object that includes the parameters necessary for sorting, pagination, and search.</param>
        /// <param name="allItems">List of elements.</param>
        /// <returns>Sorted elements.</returns>
        IQueryable<T> SortItems(W queryParameters, IQueryable<T> allItems);

        /// <summary>
        ///  The method allows to include additional fields into the collection.
        /// </summary>
        /// <param name="additionalFields">Flag responsible for enabling additional fields.</param>
        /// <param name="item">List of elements.</param>
        /// <returns>An object with additional fields.</returns>
        T GetAdditionalFields(bool additionalFields, T item);
    }
}
