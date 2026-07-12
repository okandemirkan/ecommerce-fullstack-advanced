using Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public bool Verify(string password,string hash)
        {
            return Hash(password) == hash;
        }
    }
}
