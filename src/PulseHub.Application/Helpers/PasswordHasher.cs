using System.Text.RegularExpressions;
using BCrypt.Net;

namespace PulseHub.Application.Helpers
{
    public class PasswordHasher
    {
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            var passwordRegex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, passwordRegex);
        }

        public static string HashPassword(string password)
        {
            if (!IsValidPassword(password))
            {
                throw new ArgumentException("Password does not meet security requirements.");
            }
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        // Weryfikowanie 
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
