using GSMWeb.Core.Entities;

namespace GSMWeb.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<string?> LoginAsync(string email, string password);
    }
}