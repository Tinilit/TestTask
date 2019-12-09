using System;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Api.Models
{
    public class ProductDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Required field.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [Range(1, 9999999999.99, ErrorMessage = "Enter a positive number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Required field.")]
        public Guid? CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
