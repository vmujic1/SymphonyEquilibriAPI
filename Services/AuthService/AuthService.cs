using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SymphonyEquilibriAPI.Data;
using SymphonyEquilibriAPI.Models;
using SymphonyEquilibriAPI.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SymphonyEquilibriAPI.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found!";
            }
            else if(!VerifyPasswordHash(password, user.PasswrodHash, user.PasswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wrong password.";
            } 
            else
            {
                serviceResponse.Data = CreateToken(user);
                serviceResponse.Message = "Successfully logged in!";
            }

            return serviceResponse;
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); // Token
        }
    }
}
