using Microsoft.AspNetCore.Identity;

namespace SymphonyEquilibriAPI.Models.User
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[0];

        public byte[] PasswordSalt { get; set; } = new byte[0];

        public UserRole Role { get; set; }
    }
}
