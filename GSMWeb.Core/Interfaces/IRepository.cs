using GSMWeb.Core.Entities;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}