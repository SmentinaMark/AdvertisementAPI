namespace adAPI.Contracts
{
    public interface IQueryManipulation<T,W>
    {
        IQueryable<T> SearchAdvertisements(W queryParameters, IQueryable<T> allAdvertisements);
        IQueryable<T> PagingAdvertisements(W queryParameters, IQueryable<T> allAdvertisements);
        IQueryable<T> SortAdvertisements(W queryParameters, IQueryable<T> allAdvertisements);
        T GetAdditionalFields(bool additionalFields, T advertisement);
    }
}
