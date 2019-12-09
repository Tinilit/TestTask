using Microsoft.EntityFrameworkCore;
using TestTask.DataAccess.Domain;

namespace TestTask.DataAccess
{
    public class TestTaskDbContext:DbContext
    {


        public TestTaskDbContext(DbContextOptions<TestTaskDbContext> options)
        : base(options)
        {
            
        } 

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}