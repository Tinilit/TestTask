using System;

namespace TestTask.Api.Models
{
    public class ProductListRequest
    {
        /// <summary>
        /// Requested page, starts with 0
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Filter by Title
        /// </summary>
        public string Term { get; set; }
        /// <summary>
        /// Filter by selected category
        /// </summary>
        public Guid? CategoryFilterId { get; set; }
    }
}
