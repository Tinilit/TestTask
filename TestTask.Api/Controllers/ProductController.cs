using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using TestTask.Api.ModelBuilders;
using TestTask.Api.Models;
using TestTask.Api.Models.Errors;
using TestTask.Api.Extensions;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IStringLocalizer<ProductController> _localizer;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger,
            IProductService productService,
            IStringLocalizer<ProductController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            _productService = productService;
        }

        /// <summary>
        /// Returns list of products
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of products</response> 
        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ProductListResponse), 200)]
        public IActionResult GetList([FromQuery] ProductListRequest request)
        {
            return Ok(_productService.ListResponse(request));
        }

        /// <summary>
        /// Returns product dto for editing
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Product id</param>
        /// <response code="200">ProductDto</response> 
        /// <response code="400">Validation result</response> 
        [HttpGet]
        [Route("edit/{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ValidationErrorDto), 400)]
        public ProductDto Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out Guid validation))
                throw new ValidationException(_localizer["WrongRequest"]);

            var product = _productService.GetById(Guid.Parse(id));
            if (product == null)
                throw new ValidationException(_localizer["ProductNotFound"]);

            return product;
        }

        /// <summary>
        /// Saves product
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Save product response</response> 
        /// <response code="400">Validation result</response> 
        [HttpPost]
        [Route("save")]
        [ProducesResponseType(typeof(IActionResult), 201)]
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(typeof(ValidationErrorDto), 400)]
        public IActionResult Save([FromBody] ProductDto productDto)
        {
            ModelState.ThrowValidationExceptionIfNotValid();

            if (productDto.Id == null)
                _productService.Add(productDto);
            else
                _productService.Update(productDto);

            return NoContent();
        }

        /// <summary>
        /// Deletes product
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Product id</param>
        /// <response code="204">Product deleted</response> 
        /// <response code="400">Validation result</response> 
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(typeof(IActionResult), 204)]
        [ProducesResponseType(typeof(ValidationErrorDto), 400)]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out Guid validatedId))
                throw new ValidationException(_localizer["WrongRequest"]);

            var product = _productService.GetById(Guid.Parse(id));
            if (product == null)
                throw new ValidationException(_localizer["ProductNotFound"]);

            _productService.Delete(validatedId);

            return NoContent();
        }
    }
}
