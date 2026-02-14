using System.Text;
using System.Security.Cryptography;

namespace MarShield.API.Helpers
{
    /// <summary>
    /// This class provides helper methods for security-related operations, such as password hashing and verification.
    /// </summary>
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
