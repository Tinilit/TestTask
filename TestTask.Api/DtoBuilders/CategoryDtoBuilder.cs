using System;
using TestTask.Api.Models;
using TestTask.DataAccess.Domain;

namespace TestTask.Api.DtoBuilders
{
    public static class CategoryDtoBuilder
    {
        public static CategoryDto ToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Title = category.Title
            };
        }

        public static Category ToEntity(CategoryDto product)
        {
            return new Category
            {
                Id = product.Id ?? Guid.NewGuid(),
                Title = product.Title
            };
        }
    }
}
