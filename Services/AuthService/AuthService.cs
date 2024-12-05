using Microsoft.VisualBasic;
using SymphonyEquilibriAPI.Data;
using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.User;

namespace SymphonyEquilibriAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            this._context = context;
        }
        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if(await UserExist(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswrodHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = UserRole.ProjectManager;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;

            return response;
        }

        public async Task<bool> UserExist(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username))
            {
                return true;
            }
            
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwrodSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwrodSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
