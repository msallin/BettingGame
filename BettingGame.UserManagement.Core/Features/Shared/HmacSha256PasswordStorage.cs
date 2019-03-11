using System;
using System.Security.Cryptography;

using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BettingGame.UserManagement.Core.Features.Shared
{
    internal class HmacSha256PasswordStorage : IPasswordStorage
    {
        private const int IterationCount = 10000;

        private const KeyDerivationPrf KeyDerivationAlgorithm = KeyDerivationPrf.HMACSHA256;

        private const char Separator = ';';

        public string Create(string password)
        {
            return Create(password, GenerateRandomSalt());
        }

        public bool Match(string inputPassword, string originalPassword)
        {
            string salt = originalPassword.Split(Separator)[1];
            return Create(inputPassword, Convert.FromBase64String(salt)) == originalPassword;
        }

        private static string Create(string password, byte[] salt)
        {
            byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationAlgorithm, IterationCount, 256 / 8);
            return Convert.ToBase64String(hash) + Separator + Convert.ToBase64String(salt);
        }

        private static byte[] GenerateRandomSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            var salt = new byte[128 / 8];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
