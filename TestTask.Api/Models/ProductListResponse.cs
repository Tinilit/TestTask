using System.Collections.Generic;

namespace TestTask.Api.Models
{
    public class ProductListResponse
    {
        public List<ProductDto> Values { get; set; }

        public ProductListResponse()
        {
            Values = new List<ProductDto>();
        }

        /// <summary>
        /// Total products count for selected filters
        /// </summary>
        public int Total { get; set; }
    }
}
