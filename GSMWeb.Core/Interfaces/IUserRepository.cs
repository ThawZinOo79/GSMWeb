using GSMWeb.Core.Entities;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}