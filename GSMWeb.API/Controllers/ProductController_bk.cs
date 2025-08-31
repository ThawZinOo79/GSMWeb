using GSMWeb.API.DTOs;
using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Helper method to map Product -> ProductDto
        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                GoldQuality = product.GoldQuality,
                ProductType = product.ProductType,
                EstimatePrice = product.EstimatePrice,
                Specification = product.Specification,
                Photo = product.Photo,
                User = product.User,
                CreatedBy = product.CreatedBy,
                UpdatedBy = product.UpdatedBy,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name
            };
        }

        // POST: api/product
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                ProductDescription = dto.ProductDescription,
                GoldQuality = dto.GoldQuality,
                ProductType = dto.ProductType,
                EstimatePrice = dto.EstimatePrice,
                Specification = dto.Specification,
                Photo = dto.Photo,
                User = dto.User,
                CreatedBy = dto.CreatedBy,
                CategoryId = dto.CategoryId
            };

            var createdProduct = await _productService.CreateProductAsync(product);

            if (createdProduct == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Category does not exist"
                });
            }

            var createdDto = MapToDto(createdProduct);

            return CreatedAtAction(nameof(GetProductById), new { id = createdDto.Id }, new
            {
                success = true,
                message = "Product created successfully",
                data = createdDto
            });
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound(new { success = false, message = "Product not found" });

            var dto = MapToDto(product);
            return Ok(new { success = true, data = dto });
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchString = null)
        {
            var pagingParams = new PagingParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var (products, totalCount) = await _productService.GetPaginatedProductsAsync(pagingParams, searchString);

            var productDtos = products.Select(MapToDto).ToList();

            var pagedResult = new PagedResult<ProductDto>(
                productDtos,
                pagingParams.PageNumber,
                pagingParams.PageSize,
                totalCount
            );

            return Ok(pagedResult);
        }


        // GET: api/product/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(
            int categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchString = null)
        {
            var pagingParams = new PagingParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var (products, totalCount) = await _productService
                .GetPaginatedProductsByCategoryAsync(categoryId, pagingParams, searchString);

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

            var pagedResult = new PagedResult<ProductDto>(
                productDtos,
                pagingParams.PageNumber,
                pagingParams.PageSize,
                totalCount
            );

            return Ok(pagedResult);
        }

        // GET: api/product/category
        [HttpGet("category")]
        public async Task<IActionResult> GetAllProductsByCategory(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchString = null)
        {
            var pagingParams = new PagingParameters
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // This should fetch ALL products with paging + optional search
            var (products, totalCount) = await _productService
                .GetPaginatedProductsAsync(pagingParams, searchString);

            var productDtos = products.Select(MapToDto).ToList();

            var pagedResult = new PagedResult<ProductDto>(
                productDtos,
                pagingParams.PageNumber,
                pagingParams.PageSize,
                totalCount
            );

            return Ok(pagedResult);
        }


        // PUT: api/product/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto dto)
        {
            var product = new Product
            {
                Id = id,
                ProductName = dto.ProductName,
                ProductDescription = dto.ProductDescription,
                GoldQuality = dto.GoldQuality,
                ProductType = dto.ProductType,
                EstimatePrice = dto.EstimatePrice,
                Specification = dto.Specification,
                Photo = dto.Photo,
                User = dto.User,
                UpdatedBy = dto.UpdatedBy,
                CategoryId = dto.CategoryId
            };

            var updated = await _productService.UpdateProductAsync(product);

            if (!updated) return NotFound(new { success = false, message = "Product not found" });

            var updatedProduct = await _productService.GetProductByIdAsync(id);
            var updatedDto = MapToDto(updatedProduct!);

            return Ok(new { success = true, message = "Product updated successfully", data = updatedDto });
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted) return NotFound(new { success = false, message = "Product not found" });

            return Ok(new { success = true, message = "Product deleted successfully" });
        }
    }
}
