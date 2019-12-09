using System;
using TestTask.Api.Models;
using TestTask.DataAccess.Domain;

namespace TestTask.Api.ModelBuilders
{
    public static class ProductDtoBuilder
    {
        public static ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                CategoryTitle = product.Category?.Title ?? "N/A",
                CategoryId = product.CategoryId,
                CreatedDate = product.CreatedDate
            };
        }

        public static Product ToEntity(ProductDto product)
        {
            return new Product
            {
                Id = product.Id ?? Guid.NewGuid(),
                Title = product.Title,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }
    }
}
