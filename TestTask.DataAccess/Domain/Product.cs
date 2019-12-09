using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.DataAccess.Domain
{
    public class Product:Entity
    {
        public string Title { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
