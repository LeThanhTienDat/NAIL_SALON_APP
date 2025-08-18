using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models.Helpers
{
    internal class PasswordHelper
    {
        public static string GetSalt()
        {
            byte[] salt = new byte[16];
            using(var get = new RNGCryptoServiceProvider())
            {
                get.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
            byte[] hash = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPasswrod(string enteredPassword, string storedHashPassword, string storedSalt)
        {
            string enteredHash = HashPassword(enteredPassword, storedSalt);
            return enteredHash == storedHashPassword;
        }
    }
}
