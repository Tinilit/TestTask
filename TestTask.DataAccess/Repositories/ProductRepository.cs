using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTask.DataAccess.Domain;

namespace TestTask.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Product GetById(Guid id);
        IEnumerable<Product> Get(int? page = null, string title = null, Guid? categoryFilterId = null);

        int Delete(Guid id);
        int Add(Product product);
        int Update(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private TestTaskDbContext _context;
        private readonly int _defaultPageSize = 10;

        public ProductRepository(TestTaskDbContext context)
        {
            _context = context;
        }

        public int Add(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChanges();
        }

        public int Delete(Guid id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
                return -1;

            _context.Products.Remove(product);
            return _context.SaveChanges();
        }

        public IEnumerable<Product> Get(int? page = null, string title = null, Guid? categoryFilterId = null)
        {
            var products = _context.Products
                .WithTitle(title)
                .WithCategory(categoryFilterId)
                .WithPagination(page, _defaultPageSize)
                .Include(x => x.Category);

            return products;
        }

        public Product GetById(Guid id)
        {
            var product = _context.Products.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);
            return product;
        }

        public int Update(Product product)
        {
            _context.Products.Update(product);
            return _context.SaveChanges();
        }
    }

    internal static class ProductQueryExtensions
    {
        internal static IQueryable<Product> WithTitle(this IQueryable<Product> request, string title)
        {
            if (string.IsNullOrEmpty(title))
                return request;

            return request.Where(x => x.Title.ToLower().Contains(title.ToLower()));
        }

        internal static IQueryable<Product> WithCategory(this IQueryable<Product> request, Guid? categoryFilterId)
        {
            if (categoryFilterId == null)
                return request;

            return request.Where(x => x.CategoryId == categoryFilterId);
        }

        internal static IQueryable<Product> WithPagination(this IQueryable<Product> request, int? page, int pageSize = 0)
        {
            if (page == null)
                return request.OrderBy(x => x.CreatedDate);
            else
                return request.OrderBy(x => x.CreatedDate).Skip(page.Value * pageSize).Take(pageSize);
        }
    }
}
