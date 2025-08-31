using GSMWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();
        Task<ProductCategory?> GetCategoryByIdAsync(int id);
        Task<ProductCategory> CreateCategoryAsync(ProductCategory category);
        Task<bool> UpdateCategoryAsync(ProductCategory category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<ProductCategory?> GetByIdAsync(int id);


    }
}
