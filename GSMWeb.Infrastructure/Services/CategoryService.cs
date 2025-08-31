using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProductCategory> CreateCategoryAsync(ProductCategory category)
        {
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            return category;
        }

        //public async Task<bool> DeleteCategoryAsync(int id)
        //{
        //    if (await _repo.HasProductsAsync(id))
        //        throw new Exception("Category has products, cannot delete");

        //    var category = await _repo.GetByIdAsync(id);
        //    if (category == null) return false;

        //    _repo.Delete(category);
        //    await _repo.SaveChangesAsync();
        //    return true;
        //}

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null)
                return false; // category not found

            if (await _repo.HasProductsAsync(id))
                return false; // cannot delete because products exist

            _repo.Delete(category);
            await _repo.SaveChangesAsync();
            return true; // deleted successfully
        }


        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync() => await _repo.GetAllAsync();

        public async Task<ProductCategory?> GetCategoryByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<bool> UpdateCategoryAsync(ProductCategory category)
        {
            var existing = await _repo.GetByIdAsync(category.Id);
            if (existing == null) return false;

            existing.Name = category.Name;
            existing.UpdatedAt = DateTime.UtcNow;
            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
