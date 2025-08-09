using GSMWeb.Core.Entities;

namespace GSMWeb.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<(bool IsSuccess, string Message, string? JwtToken, string? RefreshToken)> LoginAsync(string email, string password);
        Task<(bool IsSuccess, string Message)> LogoutAsync(int userId);
    }
}