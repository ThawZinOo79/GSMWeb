using GSMWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<ProductCategory>
    {
        
        Task<IEnumerable<ProductCategory>> GetAllAsync();
        Task<bool> HasProductsAsync(int categoryId);
    }
}
