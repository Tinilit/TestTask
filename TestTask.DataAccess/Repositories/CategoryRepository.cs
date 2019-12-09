using System.Collections.Generic;
using System.Linq;
using TestTask.DataAccess.Domain;

namespace TestTask.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get(string term);
    }

    public class CategoryRepository:ICategoryRepository
    {
        private TestTaskDbContext _context;

        public CategoryRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Get(string term)
        {
            if(term != null)
            {
                return _context.Categories.Where(x => x.Title.Contains(term)).ToList();
            }
            else
            {
                return _context.Categories.ToList();
            }
            
        }
    }
}
