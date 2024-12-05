using Microsoft.AspNetCore.Identity;

namespace SymphonyEquilibriAPI.Models.User
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswrodHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public UserRole Role { get; set; }
    }
}
