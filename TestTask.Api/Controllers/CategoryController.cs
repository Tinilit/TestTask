using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Api.Models;
using TestTask.Api.Services;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IStringLocalizer<CategoryController> _localizer;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger,
            ICategoryService categoryService,
            IStringLocalizer<CategoryController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Returns list of categories filtered by term
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of products</response> 
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        public IActionResult Autocomplete([FromQuery] string term)
        {
            return Ok(_categoryService.Autocomplete(term));
        }
    }
}
