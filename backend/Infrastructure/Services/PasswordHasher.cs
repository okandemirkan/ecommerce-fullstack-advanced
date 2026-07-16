using Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const string Algorithm = "PBKDF2-SHA256";
        private const int Iterations = 210_000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public string Hash(string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);

            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return $"{Algorithm}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public bool Verify(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
                return false;

            if (hash.StartsWith($"{Algorithm}$", StringComparison.Ordinal))
                return VerifyPbkdf2(password, hash);

            return VerifyLegacySha256(password, hash);
        }

        public bool NeedsRehash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash) || !hash.StartsWith($"{Algorithm}$", StringComparison.Ordinal))
                return true;

            var parts = hash.Split('$');
            return parts.Length != 4 || !int.TryParse(parts[1], out var iterations) || iterations < Iterations;
        }

        private static bool VerifyPbkdf2(string password, string encodedHash)
        {
            try
            {
                var parts = encodedHash.Split('$');
                if (parts.Length != 4 || !int.TryParse(parts[1], out var iterations) || iterations < 100_000)
                    return false;

                var salt = Convert.FromBase64String(parts[2]);
                var expectedHash = Convert.FromBase64String(parts[3]);
                var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256,
                    expectedHash.Length);

                return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static bool VerifyLegacySha256(string password, string encodedHash)
        {
            try
            {
                var expectedHash = Convert.FromBase64String(encodedHash);
                var actualHash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
                return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
