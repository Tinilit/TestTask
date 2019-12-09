using System;
using System.Linq;
using TestTask.Api.Models;
using TestTask.DataAccess.Repositories;

namespace TestTask.Api.ModelBuilders
{
    public interface IProductService
    {
        ProductListResponse ListResponse(ProductListRequest request);
        ProductDto GetById(Guid id);
        bool Add(ProductDto dto);
        bool Update(ProductDto dto);
        bool Delete(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductListResponse ListResponse(ProductListRequest request)
        {
            var products = _productRepository.Get(request.Page, request.Term, request.CategoryFilterId).ToList();
            
            return new ProductListResponse()
            {
                Values = products.Select(x => ProductDtoBuilder.ToDto(x)).ToList(),
                Total = _productRepository.Get(title:request.Term,categoryFilterId:request.CategoryFilterId).Count()
            };
        }

        public ProductDto GetById(Guid id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return null;

            return ProductDtoBuilder.ToDto(product);
        }

        public bool Add(ProductDto dto)
        {
            var product = ProductDtoBuilder.ToEntity(dto);
            return _productRepository.Add(product) == 1;
        }

        public bool Update(ProductDto dto)
        {
            var product = _productRepository.GetById(dto.Id.Value);
            product.CategoryId = dto.CategoryId;
            product.ModifiedDate = DateTime.UtcNow;
            product.Price = dto.Price;
            product.Title = dto.Title;
            return _productRepository.Update(product) == 1;
        }

        public bool Delete(Guid id)
        {
            return _productRepository.Delete(id)==1;
        }
    }
}