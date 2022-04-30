namespace adAPI.Models
{
    public class CollectionQueryParameters
    {
        #region Paging

        const int MAX_PAGE_SIZE = 50;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
            }
        }
        #endregion

        #region Search
        public string Search { get; set; }
        #endregion

        #region Sort
        public string Sort { get; set; }
        #endregion
    }
}
