using GSMWeb.API.DTOs;
using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        // ✅ Get products by category
        //GET /api/category/2?searchString=ring&pageNumber=1&pageSize=10
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsByCategory(int id, [FromQuery] PagingParameters pagingParams, [FromQuery] string? searchString)
        {
            var (products, totalCount) = await _productService.GetPaginatedProductsByCategoryAsync(id, pagingParams, searchString);

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                GoldQuality = p.GoldQuality,
                ProductType = p.ProductType,
                EstimatePrice = p.EstimatePrice,
                Specification = p.Specification,
                Photo = p.Photo,
                User = p.User,
                CreatedBy = p.CreatedBy,
                UpdatedBy = p.UpdatedBy,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            }).ToList();

            var pagedResult = new PagedResult<ProductDto>(productDtos, pagingParams.PageNumber, pagingParams.PageSize, totalCount);
            return Ok(pagedResult);
        }
        //GET /api/category?searchString=ring&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PagingParameters pagingParams, [FromQuery] string? searchString)
        {
            var (products, totalCount) = await _productService.GetPaginatedProductsAsync(pagingParams, searchString);

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                GoldQuality = p.GoldQuality,
                ProductType = p.ProductType,
                EstimatePrice = p.EstimatePrice,
                Specification = p.Specification,
                Photo = p.Photo,
                User = p.User,
                CreatedBy = p.CreatedBy,
                UpdatedBy = p.UpdatedBy,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            }).ToList();

            var pagedResult = new PagedResult<ProductDto>(productDtos, pagingParams.PageNumber, pagingParams.PageSize, totalCount);
            return Ok(pagedResult);
        }

        // ✅ Category CRUD
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] ProductCategoryDto dto)
        {
            var category = new ProductCategory { Name = dto.Name };
            var created = await _categoryService.CreateCategoryAsync(category);

            return Ok(new
            {
                success = true,
                message = "Category created successfully",
                data = created
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] ProductCategoryDto dto)
        {
            var category = new ProductCategory { Id = id, Name = dto.Name };
            var success = await _categoryService.UpdateCategoryAsync(category);
            if (!success) return NotFound();

            return Ok(new { success = true, message = "Category updated successfully" });
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    var success = await _categoryService.DeleteCategoryAsync(id);
        //    if (!success) return NotFound();

        //    return Ok(new { success = true, message = "Category deleted successfully" });
        //}

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Category not found"
                });
            }

            var success = await _categoryService.DeleteCategoryAsync(id);
            if (!success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Product with this category exists. Can't delete."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Category deleted successfully"
            });
        }




    }
}
