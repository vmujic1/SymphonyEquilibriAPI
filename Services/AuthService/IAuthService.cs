using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.User;

namespace SymphonyEquilibriAPI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<string>> Login(string username, string password);

        Task<bool> UserExist(string username);
    }
}
