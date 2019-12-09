using System.Collections.Generic;

namespace TestTask.DataAccess.Domain
{
    public class Category:Entity
    {
        public string Title { get; set; }

        public List<Product> Products { get; set; }
    }
}
