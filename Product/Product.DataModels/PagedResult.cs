using System.Collections.Generic;

namespace Product.DataModels
{
    /// <summary>
    /// Paged result generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Result value
        /// </summary>
        public List<T> Result { get; set; }
        /// <summary>
        /// Zero based page index
        /// </summary>
        public int CurrentPageIndex { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }

        public PagedResult()
        {
            TotalPages = 1;
        }
    }
}
