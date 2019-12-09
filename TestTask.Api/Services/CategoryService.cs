using System.Collections.Generic;
using System.Linq;
using TestTask.Api.DtoBuilders;
using TestTask.Api.Models;
using TestTask.DataAccess.Repositories;

namespace TestTask.Api.Services
{
    public interface ICategoryService
    {
        List<CategoryDto> Autocomplete(string term);
    }

    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<CategoryDto> Autocomplete(string term)
        {
            return _categoryRepository.Get(term).Select(x=>CategoryDtoBuilder.ToDto(x)).ToList();
        }
    }
}
