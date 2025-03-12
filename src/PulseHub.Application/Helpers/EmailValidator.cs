using System.Text.RegularExpressions;

namespace PulseHub.Application.Helpers
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            var emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegex);
        }
    }
}
