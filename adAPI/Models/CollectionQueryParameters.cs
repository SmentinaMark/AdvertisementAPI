using Swashbuckle.AspNetCore.Annotations;

namespace adAPI.Models
{
    public class CollectionQueryParameters
    {
        #region Paging

        const int MAX_PAGE_SIZE = 50;
        private int _pageSize = 10;

        /// <summary>
        /// Allow to navigate through pages. Optional parameter. Default value = 1.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Allow to set count of elements of the page. Optional parameter. Default value = 10. Max value = 50;
        /// </summary>
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
        /// <summary>
        /// Set a property for the nessecery search element. Optional parameter.
        /// </summary>
        public string Search { get; set; }
        #endregion

        #region Sort
        /// <summary>
        /// Set for sort by price_asc/desc, date_asc/desc. Optional parameter. Default value = sort ASC by title.
        /// </summary>
        public string Sort { get; set; }
        #endregion
    }
}
